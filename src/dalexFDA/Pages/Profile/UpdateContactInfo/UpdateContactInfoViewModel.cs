using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PropertyChanged;
using System.Linq;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using System.Threading.Tasks;

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
                
        public List<Lookup> ContactTypes { get; set; }
        public int? SelectedContactTypeIndex { get; set; }
        public Lookup SelectedContactType => SelectedContactTypeIndex != null ? ContactTypes?.ToList()[SelectedContactTypeIndex.GetValueOrDefault()] : null;
        public bool ContactTypeHasError { get; set; }
        public string ContactTypeErrorMessage { get; set; }

        public bool IsPhoneNumber { get; set; }
        public string CurrentPhoneNumber { get; set; }
        public string NewPhoneNumber { get; set; }
        public bool NewPhoneNumberHasError { get; set; }
        public string NewPhoneNumberErrorMessage { get; set; }

        public bool IsEmailAddress { get { return !IsPhoneNumber; } }
        public string CurrentEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
        public bool NewEmailAddressHasError { get; set; }
        public string NewEmailAddressMessage { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public Command Submit { get; set; }
        public Command Validate { get; private set; }
        public Command ContactTypeChanged { get; set; }

        private const string contact_type_error_message = "Please select a contact type.";
        private const string new_phone_error_message = "Please a phone number.";
        private const string new_email_error_message = "Please provide a valid email address.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public UpdateContactInfoViewModel(IErrorManager ErrorManager, ILookupService LookupService, ISession SessionService, IInvestmentService IInvestmentService,
                                    IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;
            this.LookupService = LookupService;

            Submit = new Command(async () => await ExecuteSubmit());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
            ContactTypeChanged = new Command<ValidationCommandNav>(async (obj) => await ExecuteContactTypeChanged(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                ContactTypes = await LookupService.GetContactTypes();
                CurrentEmailAddress = SessionService.CurrentUser.Email;
                CurrentPhoneNumber = SessionService.CurrentUser?.PhoneNumber;
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
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

                using (Dialog.Loading("Loading..."))
                {
                    var request = new AccountStatementRequest
                    {
                        StartDate = StartDate,
                        EndDate = EndDate,
                        ContactType = SelectedContactType?.Name,
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
                case "ContactType":
                    ContactTypeHasError = SelectedContactType == null;
                    ContactTypeErrorMessage = contact_type_error_message;
                    break;
                case "NewPhoneNumber":
                    NewPhoneNumberHasError = false;
                    break;
                case "NewEmailAddress":
                    NewEmailAddressHasError = false;
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

            NewPhoneNumberHasError = string.IsNullOrEmpty(NewPhoneNumber);
            NewPhoneNumberErrorMessage = new_phone_error_message;

            NewEmailAddressHasError = string.IsNullOrEmpty(NewEmailAddress);
            NewEmailAddressMessage = new_email_error_message;

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

            return NewPhoneNumberHasError  SecurityAnswerHasError || ContactTypeHasError;
        }
    }
}
