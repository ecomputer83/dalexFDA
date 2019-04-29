using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Linq;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class FeedbackViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IUserDialogs Dialog;
        readonly IAccountService AccountService;
        readonly ISession SessionService;
        readonly ILookupService LookupService;

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public string ClientName { get; set; }

        public string Message { get; set; }
        public bool MessageHasError { get; set; }
        public string MessageErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }
        public string SecurityHint { get; set; }
        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public List<Lookup> MessageTypes { get; set; }
        public int? SelectedMessageTypeIndex { get; set; }
        public Lookup SelectedMessageType => SelectedMessageTypeIndex != null ? MessageTypes?.ToList()[SelectedMessageTypeIndex.GetValueOrDefault()] : null;
        public bool MessageTypeHasError { get; set; }
        public string MessageTypeErrorMessage { get; set; }

        public Command Negotiate { get; set; }
        public Command Done { get; set; }
        public Command Validate { get; private set; }

        private const string message_type_error_message = "Please select a message type.";
        private const string message_error_message = "Message can not be empty.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public FeedbackViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog, ILookupService LookupService,
                                                ISession SessionService, IAccountService accountService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.LookupService = LookupService;
            this.Dialog = Dialog;
            this.SessionService = SessionService;
            this.AccountService = accountService;

            Negotiate = new Command(async () => await ExecuteNegotiate());
            Done = new Command(async () => await ExecuteDone());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                MessageTypes = await this.LookupService.GetMessageTypes();
                ClientName = SessionService?.CurrentUser?.Name;
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
                SecurityHint = "Hint: " + SessionService?.CurrentUser?.SecurityHint;
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
                    var request = new QueryRequest
                    {
                        Client_No = SessionService?.CurrentUser?.ClientNo,
                        Client_Name = SessionService?.CurrentUser?.Name,
                        Email_Address = SessionService?.CurrentUser?.Email,
                        Message_Detail = Message
                    };
                    string response = await AccountService.PostQuery(int.Parse(SelectedMessageType.Code), request);

                    if (response != null)
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
                case "MessageType":
                    MessageTypeHasError = SelectedMessageType == null;
                    MessageTypeErrorMessage = message_type_error_message;
                    break;
                case "Message":
                    MessageHasError = string.IsNullOrEmpty(Message);
                    MessageErrorMessage = message_error_message;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            MessageTypeHasError = SelectedMessageType == null;
            MessageTypeErrorMessage = message_type_error_message;

            MessageHasError = string.IsNullOrEmpty(Message);
            MessageErrorMessage = message_error_message;

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

            return MessageTypeHasError || MessageHasError || SecurityAnswerHasError;
        }
    }
}
