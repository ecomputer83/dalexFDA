using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PropertyChanged;
using System.Linq;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class UpdateContactInfoViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;
        readonly ILookupService LookupService;
        readonly IAppService AppService;
                
        public List<Lookup> ContactTypes { get; set; }
        public int? SelectedContactTypeIndex { get; set; }
        public Lookup SelectedContactType => SelectedContactTypeIndex != null ? ContactTypes?.ToList()[SelectedContactTypeIndex.GetValueOrDefault()] : null;
        public bool ContactTypeHasError { get; set; }
        public string ContactTypeErrorMessage { get; set; }

        public string OldValueLabel { get { return SelectedContactType.Code == RequestType.Phone ? old_phone_number_text : old_email_address_text; } }
        public string NewValueLabel { get { return SelectedContactType.Code == RequestType.Phone ? new_phone_number_text : new_email_address_text; } }

        public string OldValueText { get { return SelectedContactType.Code == RequestType.Phone ? SessionService?.CurrentUser?.PhoneNumber : SessionService?.CurrentUser?.Email; } }
        public string NewValueText { get; set; }

        public bool NewValueTextHasError { get; set; }
        public string NewValueTextErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public Command Submit { get; set; }
        public Command Validate { get; private set; }
        public Command ContactTypeChanged { get; set; }

        private const string old_phone_number_text = "Current Phone Number";
        private const string old_email_address_text = "Current Email Address";
        private const string new_phone_number_text = "New Phone Number";
        private const string new_email_address_text = "New Email Address";

        private const string contact_type_error_message = "Please select a contact type.";
        private const string new_phone_error_message = "Please enter a phone number.";
        private const string new_email_error_message = "Please provide a valid email address.";
        private const string invalid_email_address_error_message = "Please enter a valid email address.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public UpdateContactInfoViewModel(IErrorManager ErrorManager, ILookupService LookupService, ISession SessionService, IInvestmentService IInvestmentService,
                                    IUserDialogs Dialog, IAppService AppService)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;
            this.LookupService = LookupService;
            this.AppService = AppService;

            Submit = new Command(async () => await ExecuteSubmit());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
            ContactTypeChanged = new Command(async () => await ExecuteContactTypeChanged());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                ContactTypes = await LookupService.GetContactTypes();
                SelectedContactTypeIndex = 0;
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        public async override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            try
            {
                var data = returnedData as SuccessMessageViewModel;
                AppService.StartDashboard();
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
                    var request = new ContactChangeRequest
                    {
                        NewValue = NewValueText,
                        OldValue = OldValueText,
                        RequestType = Convert.ToInt16(SelectedContactType.Code),
                        SecurityAnswer = SecurityAnswer
                    };
                    response = await IInvestmentService.UpdateContact(request);
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

        private async Task ExecuteContactTypeChanged()
        {
            try
            {
                if (SelectedContactType != null)
                {
                    if (SelectedContactType.Code == RequestType.Phone)
                    {
                        NewValueText = "";
                    }
                    else
                    {
                        NewValueText = "";
                    }
                }
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
                case "ContactType":
                    ContactTypeHasError = SelectedContactType == null;
                    ContactTypeErrorMessage = contact_type_error_message;
                    NewValueTextHasError = false;
                    break;
                case "NewValue":
                    NewValueTextHasError = false;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        public bool PerformValidation()
        {
            ContactTypeHasError = SelectedContactType == null;
            ContactTypeErrorMessage = contact_type_error_message;
            
            if (SelectedContactType.Code == RequestType.Phone)
            {
                NewValueTextHasError = string.IsNullOrEmpty(NewValueText);
                NewValueTextErrorMessage = new_phone_error_message;
            }
            else
            {
                NewValueTextHasError = string.IsNullOrEmpty(NewValueText);
                NewValueTextErrorMessage = new_email_error_message;

                if (!NewValueTextHasError)
                {
                    NewValueTextHasError = !ValidateEmail(NewValueText);
                    NewValueTextErrorMessage = invalid_email_address_error_message;
                }
            }

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

            return NewValueTextHasError || SecurityAnswerHasError || ContactTypeHasError;
        }

        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email,
                                         @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                                         RegexOptions.IgnoreCase);
        }
    }
}
