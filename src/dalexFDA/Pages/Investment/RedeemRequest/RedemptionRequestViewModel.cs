using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class RedemptionRequestViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;

        public InvestmentItem Investment { get; set; }

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }
        public string AccountName { get; set; }

        public double RedemptionAmount { get; set; }
        public bool RedemptionAmountHasError { get; set; }
        public string RedemptionAmountErrorMessage { get; set; }

        public double Redemption { get; set; }
        public double ReinvestmentAmount {
            get {
                double result = 0;
                if (Investment?.Redemption > 0)
                {
                    result = Investment.Redemption - RedemptionAmount;
                }
                if (result < 0.1)
                    result = 0;

                return result;
            }
        }

        public int NewDuration { get; set; }
        public bool NewDurationHasError { get; set; }
        public string NewDurationErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }
        public string SecurityHint { get; set; }
        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public bool IsNotFirstRun { get; set; }

        public Command Negotiate { get; set; }
        public Command Validate { get; private set; }

        Nav Data;
        public class Nav
        {
            public InvestmentItem Investment { get; set; }
        }

        private const string redemption_amount_error_message = "Redeem Amount cannot be greater than expected";
        private const string new_duration_error_message = "Please enter a valid number of days.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public RedemptionRequestViewModel(IErrorManager ErrorManager, ISession SessionService, IInvestmentService IInvestmentService, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;

            Negotiate = new Command(async () => await ExecuteNegotiate());
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
                    Investment = Data.Investment;
                }
                AccountName = SessionService?.CurrentUser?.Name;
                RedemptionAmount = 0;
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
                SecurityHint = "Hint: " + SessionService?.CurrentUser?.SecurityHint;
                IsNotFirstRun = true;
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

                bool response;

                using (Dialog.Loading("Redeeming..."))
                {
                    var request = new RedeemInvestmentRequest
                    {
                        InvestmentId = Investment.Id,
                        RedemptionAmount = RedemptionAmount,
                        ReinvestmentAmount = ReinvestmentAmount,
                        Duration = Convert.ToInt32(NewDuration),
                        SecurityAnswer = SecurityAnswer
                    };
                    response = await IInvestmentService.RedeemInvestment(request);
                }

                if (response)
                    IsSuccessful = true;
                else
                    await CoreMethods.DisplayAlert("Oops", "Operation failed. Please try again", "Ok");
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
                case "NewDuration":
                    NewDurationHasError = false;
                    NewDurationErrorMessage = "";
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            RedemptionAmountHasError = RedemptionAmount > Investment.Redemption;
            RedemptionAmountErrorMessage = redemption_amount_error_message;

            NewDurationHasError = NewDuration <= 0 && ReinvestmentAmount > 0;
            NewDurationErrorMessage = new_duration_error_message;

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

            return SecurityAnswerHasError || NewDurationHasError || RedemptionAmountHasError;
        }
    }
}
