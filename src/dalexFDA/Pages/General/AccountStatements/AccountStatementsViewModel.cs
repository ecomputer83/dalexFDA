using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    [AddINotifyPropertyChangedInterface]
    public class AccountStatementsViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;
        readonly ILookupService LookupService;

        public DateTime StartDate { get; set; }
        public bool StartDateHasError { get; set; }
        public string StartDateErrorMessage { get; set; }

        public DateTime EndDate { get; set; }
        public bool EndDateHasError { get; set; }
        public string EndDateErrorMessage { get; set; }

        public List<Lookup> DeliveryModes { get; set; }
        public int? SelectedDeliveryModeIndex { get; set; }
        public Lookup SelectedDeliveryMode => SelectedDeliveryModeIndex != null ? DeliveryModes?.ToList()[SelectedDeliveryModeIndex.GetValueOrDefault()] : null;
        public bool DeliveryModeHasError { get; set; }
        public string DeliveryModeErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public Command Submit { get; set; }
        public Command Validate { get; private set; }

        private const string delivery_mode_error_message = "Please select a delivery mode.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public AccountStatementsViewModel(IErrorManager ErrorManager, ILookupService LookupService, ISession SessionService, IInvestmentService IInvestmentService,
                                    IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;
            this.LookupService = LookupService;

            Submit = new Command(async () => await ExecuteSubmit());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                DeliveryModes = await LookupService.GetDeliveryModes();
                SelectedDeliveryModeIndex = 0;
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
                StartDate = EndDate = DateTime.Today;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteSubmit()
        {
            try
            {
                if (PerformValidation()) return;

                bool response;

                using (Dialog.Loading("Please wait..."))
                {
                    var request = new StatementRequest
                    {
                        StartDate = StartDate,
                        EndDate = EndDate,
                        DeliveryMode = SelectedDeliveryMode?.Code,
                        SecurityAnswer = SecurityAnswer
                    };
                    response = await IInvestmentService.RequestAccountStatement(request);
                }

                if (response)
                {
                    var nav = new SuccessMessageViewModel.Nav { Message = "Request submitted successfully" };
                    await CoreMethods.PushPageModel<SuccessMessageViewModel>(nav, true);
                }
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
                    DeliveryModeErrorMessage = delivery_mode_error_message;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            DeliveryModeHasError = SelectedDeliveryMode == null;
            DeliveryModeErrorMessage = delivery_mode_error_message;

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

            return SecurityAnswerHasError || DeliveryModeHasError;
        }
    }
}
