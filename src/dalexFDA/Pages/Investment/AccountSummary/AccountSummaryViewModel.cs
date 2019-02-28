using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class AccountSummaryViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public DateTime StartDate { get; set; }
        public bool StartDateHasError { get; set; }
        public string StartDateHasErrorMessage { get; set; }

        public DateTime EndDate { get; set; }
        public bool EndDateHasError { get; set; }
        public string EndDateHasErrorMessage { get; set; }

        public List<Mode> DeliveryMode { get; set; }
        public int? SelectedDeliveryModeIndex { get; set; }
        public Mode SelectedDeliveryMode => SelectedDeliveryModeIndex != null ? DeliveryMode?.ToList()[SelectedDeliveryModeIndex.GetValueOrDefault()] : null;
        public bool DeliveryModeHasError { get; set; }
        public string DeliveryModeErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public Command Negotiate { get; set; }
        public Command Validate { get; private set; }

        Nav Data;
        public class Nav
        {
            public InvestmentItem Investment { get; set; }
        }
        private const string mode_error_message = "Please select a delivery mode.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public AccountSummaryViewModel(IErrorManager ErrorManager, ISession SessionService, IInvestmentService IInvestmentService, IUserDialogs Dialog)
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
                DeliveryMode = new List<Mode>(); DeliveryMode.Add(new Mode { Code = "E-Mail", Name = "Email" });
                StartDate = DateTime.Now;
                EndDate = DateTime.Now;
                SelectedDeliveryModeIndex = 0;
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

                bool response;

                using (Dialog.Loading("Submit request..."))
                {
                    var request = new StatementRequest
                    {
                        StartDate = StartDate,
                        EndDate = EndDate,
                        DeliveryMode = SelectedDeliveryMode?.Code,
                        SecurityAnswer = SecurityAnswer
                    };
                    response = await IInvestmentService.PostStatement(request);
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
                case "DeliveryMode":
                    DeliveryModeHasError = SelectedDeliveryMode == null;
                    DeliveryModeErrorMessage = mode_error_message;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
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

            return SecurityAnswerHasError;
        }
    }
}
