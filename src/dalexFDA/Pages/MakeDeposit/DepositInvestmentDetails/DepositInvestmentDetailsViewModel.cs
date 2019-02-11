using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class DepositInvestmentDetailsViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IUserDialogs Dialog;
        readonly IInvestmentService IInvestmentService;

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public DateTime TransactionDate { get; set; }
        public bool TransactionDateHasError { get; set; }
        public string TransactionDateErrorMessage { get; set; }

        public string RefNumber { get; set; }
        public bool RefNumberHasError { get; set; }
        public string RefNumberErrorMessage { get; set; }

        public double InvestmentAmount { get; set; }
        public bool InvestmentAmountHasError { get; set; }
        public string InvestmentAmountErrorMessage { get; set; }

        public string Duration { get; set; }
        public bool DurationHasError { get; set; }
        public string DurationErrorMessage { get; set; }

        public ETransferRequest ETransferRequest { get; set; }

        public Command Negotiate { get; set; }
        public Command Done { get; set; }
        public Command Validate { get; private set; }

        Nav Data;
        public class Nav
        {
            public ETransferRequest ETransferRequest { get; set; }
        }

        private const string ref_number_error_message = "Please enter a payment reference number.";
        private const string investment_amount_error_message = "Please enter an amount.";
        private const string duration_error_message = "Please enter a valid number of days.";
        
        public DepositInvestmentDetailsViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog,
                                                IInvestmentService IInvestmentService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.IInvestmentService = IInvestmentService;
            
            Negotiate = new Command(async () => await ExecuteNegotiate());
            Done = new Command(async () => await ExecuteDone());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;
                if(Data != null)
                {
                    ETransferRequest = Data.ETransferRequest;
                }
                TransactionDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteNegotiate()
        {
            try
            {
                IsSuccessful = true;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteDone()
        {
            try
            {
                if (PerformValidation()) return;

                using (Dialog.Loading("Loading..."))
                {
                    ETransferRequest.TransactionDate = TransactionDate;
                    ETransferRequest.RefNumber = RefNumber;
                    ETransferRequest.InvestmentAmount = InvestmentAmount;
                    ETransferRequest.Duration = Convert.ToInt32(Duration);

                    bool response = await IInvestmentService.DepositEInvestment(ETransferRequest);

                    if (response)
                        AppService.StartMainFlow();
                    else
                        await CoreMethods.DisplayAlert("Oops", "Operation failed. Please try again", "Ok");
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
                case "RefNumber":
                    RefNumberHasError = string.IsNullOrEmpty(RefNumber);
                    RefNumberErrorMessage = ref_number_error_message;
                    break;
                case "InvestmentAmount":
                    InvestmentAmountHasError = InvestmentAmount <= 0;
                    InvestmentAmountErrorMessage = investment_amount_error_message;
                    break;
                case "Duration":
                    DurationHasError = string.IsNullOrEmpty(Duration) || Convert.ToInt32(Duration) <= 0;
                    DurationErrorMessage = duration_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            RefNumberHasError = string.IsNullOrEmpty(RefNumber);
            RefNumberErrorMessage = ref_number_error_message;

            InvestmentAmountHasError = InvestmentAmount <= 0;
            InvestmentAmountErrorMessage = investment_amount_error_message;

            DurationHasError = string.IsNullOrEmpty(Duration) || Convert.ToInt32(Duration) <= 0;
            DurationErrorMessage = duration_error_message;

            return RefNumberHasError || InvestmentAmountHasError || DurationHasError;
        }
    }
}
