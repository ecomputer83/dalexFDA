using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using Acr.UserDialogs;

namespace dalexFDA.Core
{
    [AddINotifyPropertyChangedInterface]
    public class DepositPaymentViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IUserDialogs Dialog;

        public double Deposit { get; set; }
        public bool DepositHasError { get; set; }
        public string DepositErrorMessage { get; set; }

        public string TransactionFee { get { return Deposit > 0 ? NumberFormatter.FormatAmount((Deposit * 0.015).ToString()) : "0.00"; } }

        public bool IsBank { get { return !IsCard; } }
        public bool IsCard { get; set; }

        //commands
        public Command Bank { get; private set; }
        public Command Card { get; private set; }
        public Command Continue { get; private set; }
        public Command Validate { get; private set; }

        private const string deposit_amount_error_message = "Please enter an amount.";

        public DepositPaymentViewModel(IErrorManager ErrorManager, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.Dialog = Dialog;

            Bank = new Command(async () => await ExecuteBank());
            Card = new Command(async () => await ExecuteCard());
            Continue = new Command(async () => await ExecuteContinue());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        private async Task ExecuteBank()
        {
            try
            {
                IsCard = false;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteCard()
        {
            try
            {
                IsCard = true;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteContinue()
        {
            try
            {
                if (PerformValidation()) return;

                using (Dialog.Loading("Loading..."))
                {
                    var request = new ETransferRequest
                    {
                        DepositAmount = Deposit
                    };
                    var nav = new CardPaymentDetailsViewModel.Nav { ETransferRequest = request };
                    await CoreMethods.PushPageModel<CardPaymentDetailsViewModel>(nav);
                }                
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteValidate(ValidationCommandNav obj)
        {
            try
            {
                ValidateControls(obj?.Name);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private void ValidateControls(string name)
        {
            switch (name)
            {
                case "Deposit":
                    DepositHasError = false;
                    DepositErrorMessage = DepositHasError ? deposit_amount_error_message : "";
                    break;
            }
        }

        public bool PerformValidation()
        {
            DepositHasError = Deposit <= 0;
            DepositErrorMessage = deposit_amount_error_message;

            return DepositHasError;
        }
    }
}
