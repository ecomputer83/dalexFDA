using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using Acr.UserDialogs;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class DepositPaymentViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IUserDialogs Dialog;

        public string Beneficiary { get; set; }
        public bool BeneficiaryHasError { get; set; }
        public string BeneficiaryErrorMessage { get; set; }

        public string PaymentPurpose { get; set; }
        public bool PaymentPurposeHasError { get; set; }
        public string PaymentPurposeErrorMessage { get; set; }

        public double Deposit { get; set; }
        public bool DepositHasError { get; set; }
        public string DepositErrorMessage { get; set; }

        public double TransactionFee { get; set; }
        public bool TransactionFeeHasError { get; set; }
        public string TransactionFeeErrorMessage { get; set; }

        public bool IsBank { get { return !IsCard; } }
        public bool IsCard { get; set; }

        //commands
        public Command Bank { get; private set; }
        public Command Card { get; private set; }
        public Command Continue { get; private set; }
        public Command Validate { get; private set; }

        private const string beneficiary_account_name_error_message = "Please enter the beneficiary's account name.";
        private const string payment_purpose_error_message = "Please enter a purpose for this transaction.";
        private const string deposit_amount_error_message = "Please enter an amount.";
        private const string transfer_fee_error_message = "Please enter an amount.";

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
                        BeneficiaryAccountName = Beneficiary,
                        PaymentPurpose = PaymentPurpose,
                        DepositAmount = Deposit,
                        TransferFee = TransactionFee,
                        PaymentMethod = IsBank ? "Bank" : "Card"
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
                case "Beneficiary":
                    BeneficiaryHasError = string.IsNullOrEmpty(Beneficiary);
                    BeneficiaryErrorMessage = beneficiary_account_name_error_message;
                    break;
                case "PaymentPurpose":
                    PaymentPurposeHasError = string.IsNullOrEmpty(PaymentPurpose);
                    PaymentPurposeErrorMessage = payment_purpose_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            BeneficiaryHasError = string.IsNullOrEmpty(Beneficiary);
            BeneficiaryErrorMessage = beneficiary_account_name_error_message;

            PaymentPurposeHasError = string.IsNullOrEmpty(PaymentPurpose);
            PaymentPurposeErrorMessage = payment_purpose_error_message;

            DepositHasError = Deposit <= 0;
            DepositErrorMessage = deposit_amount_error_message;

            TransactionFeeHasError = TransactionFee <= 0;
            TransactionFeeErrorMessage = transfer_fee_error_message;

            return BeneficiaryHasError || PaymentPurposeHasError || DepositHasError || TransactionFeeHasError;
        }
    }
}
