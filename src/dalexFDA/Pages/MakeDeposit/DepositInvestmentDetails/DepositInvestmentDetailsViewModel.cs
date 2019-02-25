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
        readonly ISession SessionService;

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

        public int Duration { get; set; }
        public bool DurationHasError { get; set; }
        public string DurationErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

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
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public DepositInvestmentDetailsViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog,
                                                IInvestmentService IInvestmentService, ISession SessionService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.IInvestmentService = IInvestmentService;
            this.SessionService = SessionService;
            
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
                    RefNumber = ETransferRequest.PaymentReference;
                }
                TransactionDate = DateTime.Now;
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
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
                if (PerformValidation()) return;

                using (Dialog.Loading("Loading..."))
                {
                    ETransferRequest.DepositDate = TransactionDate;
                    ETransferRequest.InvestmentAmount = InvestmentAmount;
                    ETransferRequest.Duration = Convert.ToInt32(Duration);
                    ETransferRequest.SecurityAnswer = SecurityAnswer;

                    bool response = await IInvestmentService.DepositEInvestment(ETransferRequest);

                    if (response)
                        IsSuccessful = true;
                    else
                        await CoreMethods.DisplayAlert("Oops", "Operation failed. Please try again", "Ok");
                }
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
                using (Dialog.Loading("Loading..."))
                {
                    AppService.StartMainFlow();
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
                    InvestmentAmountHasError = string.IsNullOrEmpty(InvestmentAmount.ToString());
                    InvestmentAmountErrorMessage = investment_amount_error_message;
                    break;
                case "Duration":
                    DurationHasError = string.IsNullOrEmpty(Duration.ToString());
                    DurationErrorMessage = duration_error_message;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            RefNumberHasError = string.IsNullOrEmpty(RefNumber);
            RefNumberErrorMessage = ref_number_error_message;

            InvestmentAmountHasError = InvestmentAmount <= 0;
            InvestmentAmountErrorMessage = investment_amount_error_message;

            DurationHasError = Duration <= 0;
            DurationErrorMessage = duration_error_message;

            SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
            SecurityAnswerErrorMessage = security_answer_error_message;

            if (!SecurityAnswerHasError)
            {
                if (SecurityAnswer.ToLower() != SessionService?.CurrentUser?.SecurityAnswer.ToLower())
                {
                    SecurityAnswerHasError = true;
                    SecurityAnswerErrorMessage = wrong_security_answer_error_message;
                }
            }

            return RefNumberHasError || InvestmentAmountHasError || DurationHasError || SecurityAnswerHasError;
        }
    }
}
