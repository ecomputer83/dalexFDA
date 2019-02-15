using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class CardPaymentDetailsViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IUserDialogs Dialog;

        public string DebitAmount { get; set; }
        public bool DebitAmountHasError { get; set; }
        public string DebitAmountErrorMessage { get; set; }

        public string CardNumber { get; set; }
        public bool CardNumberHasError { get; set; }
        public string CardNumberErrorMessage { get; set; }

        public string CardHolderName { get; set; }
        public bool CardHolderNameHasError { get; set; }
        public string CardHolderNameErrorMessage { get; set; }

        public string ExpiryDate { get; set; }
        public bool ExpiryDateHasError { get; set; }
        public string ExpiryDateErrorMessage { get; set; }

        public string CVV { get; set; }
        public bool CVVHasError { get; set; }
        public string CVVErrorMessage { get; set; }

        public string PIN { get; set; }
        public bool PINHasError { get; set; }
        public string PINErrorMessage { get; set; }

        public ETransferRequest ETransferRequest { get; set; }

        public Command Pay { get; private set; }
        public Command Validate { get; private set; }

        Nav Data;
        public class Nav
        {
            public ETransferRequest ETransferRequest { get; set; }
        }

        private const string debit_amount_error_message = "Please enter an amount.";
        private const string card_number_error_message = "Please enter your card number.";
        private const string card_holder_name_error_message = "Please enter a name.";
        private const string expiry_date_error_message = "Please enter a valid date.";
        private const string cvv_error_message = "Please enter a value.";
        private const string pin_error_message = "Please enter your pin.";

        public CardPaymentDetailsViewModel(IErrorManager ErrorManager, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.Dialog = Dialog;

            Pay = new Command(async () => await ExecutePay());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;
                if (Data != null)
                {
                    ETransferRequest = Data.ETransferRequest;
                    DebitAmount = NumberFormatter.FormatAmount(ETransferRequest.DepositAmount.ToString());
                }                
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecutePay()
        {
            try
            {
                if (PerformValidation()) return;

                using (Dialog.Loading("Loading..."))
                {
                    ETransferRequest.PaymentReference = "MC-1520443531487";

                    var nav = new DepositInvestmentDetailsViewModel.Nav { ETransferRequest = this.ETransferRequest };
                    await CoreMethods.PushPageModel<DepositInvestmentDetailsViewModel>(nav);
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
                case "CardNumber":
                    CardNumberHasError = string.IsNullOrEmpty(CardNumber);
                    CardNumberErrorMessage = card_number_error_message;
                    break;
                case "CardHolderName":
                    CardHolderNameHasError = string.IsNullOrEmpty(CardHolderName);
                    CardHolderNameErrorMessage = card_holder_name_error_message;
                    break;
                case "ExpiryDate":
                    ExpiryDateHasError = string.IsNullOrEmpty(ExpiryDate);
                    ExpiryDateErrorMessage = expiry_date_error_message;
                    break;
                case "CVV":
                    CVVHasError = string.IsNullOrEmpty(CVV);
                    CVVErrorMessage = cvv_error_message;
                    break;
                case "PIN":
                    PINHasError = string.IsNullOrEmpty(PIN);
                    PINErrorMessage = pin_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            CardNumberHasError = string.IsNullOrEmpty(CardNumber);
            CardNumberErrorMessage = card_number_error_message;

            CardHolderNameHasError = string.IsNullOrEmpty(CardHolderName);
            CardHolderNameErrorMessage = card_holder_name_error_message;

            ExpiryDateHasError = string.IsNullOrEmpty(ExpiryDate);
            ExpiryDateErrorMessage = expiry_date_error_message;

            CVVHasError = string.IsNullOrEmpty(CVV);
            CVVErrorMessage = cvv_error_message;

            PINHasError = string.IsNullOrEmpty(PIN);
            PINErrorMessage = pin_error_message;

            return CardNumberHasError || CardHolderNameHasError || ExpiryDateHasError || CVVHasError || PINHasError;
        }
    }
}
