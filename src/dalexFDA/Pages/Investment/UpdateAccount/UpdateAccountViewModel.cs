using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    [AddINotifyPropertyChangedInterface]
    class UpdateAccountViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public string OldLabel { get; set; }
        public string OldValue { get; set; }

        public string NewLabel { get; set; }
        public string NewValue { get; set; }
        public bool NewValueHasError { get; set; }
        public string NewValueErrorMessage { get; set; }

        public List<ReqType> RequestType { get; set; }
        public int? SelectedRequestTypeIndex { get; set; }
        public ReqType SelectedRequestType => SelectedRequestTypeIndex != null ? RequestType?.ToList()[SelectedRequestTypeIndex.GetValueOrDefault()] : null;
        public bool RequestTypeHasError { get; set; }
        public string RequestTypeErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public Command Negotiate { get; set; }
        public Command Validate { get; private set; }
        public Command PickedChangedCommand { get; set; }

        Nav Data;
        public class Nav
        {
            public InvestmentItem Investment { get; set; }
        }
        private const string mode_error_message = "Please select a contact type.";
        private const string newvalue_error_message = "Please enter the new contact";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public UpdateAccountViewModel(IErrorManager ErrorManager, ISession SessionService, IInvestmentService IInvestmentService, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;

            Negotiate = new Command(async () => await ExecuteNegotiate());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
            PickedChangedCommand = new Command(() => PickedChanged());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                RequestType = new List<ReqType>(); RequestType.Add(new ReqType { Code = 1, Name = "Phone No" }); RequestType.Add(new ReqType { Code = 2, Name = "Email" });

                SelectedRequestTypeIndex = 0;
                OldLabel = "Current Phone Number";
                NewLabel = "New Phone Number";
                OldValue = SessionService?.CurrentUser?.PhoneNumber;

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
                    var request = new ContactChangeRequest
                    {
                        NewValue = NewValue,
                        OldValue = OldValue,
                        RequestType = SelectedRequestType.Code,
                        SecurityAnswer = SecurityAnswer
                    };
                    response = await IInvestmentService.UpdateContact(request);
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

        private void PickedChanged()
        {
            if(SelectedRequestType != null)
            {
                if(SelectedRequestType.Code == 1)
                {
                    NewLabel = "New Phone Number";
                    OldLabel = "Current Phone Number";
                    OldValue = SessionService?.CurrentUser?.PhoneNumber;
                }
                else
                {
                    NewLabel = "New Email Address";
                    OldLabel = "Current Email Address";
                    OldValue = SessionService?.CurrentUser?.Email;
                }
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
                case "RequestType":
                    RequestTypeHasError = SelectedRequestType == null;
                    RequestTypeErrorMessage = mode_error_message;
                    break;
                case "NewValue":
                    NewValueHasError = string.IsNullOrEmpty(NewValue);
                    NewValueErrorMessage = newvalue_error_message;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            NewValueHasError = string.IsNullOrEmpty(NewValue);
            NewValueErrorMessage = newvalue_error_message;

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

            return SecurityAnswerHasError || NewValueHasError;
        }
    }
}
