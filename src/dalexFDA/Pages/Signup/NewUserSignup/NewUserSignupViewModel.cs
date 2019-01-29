using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using System.Text.RegularExpressions;
using Plugin.DeviceInfo.Abstractions;
using System.Diagnostics;
using Acr.UserDialogs;
using Refit;
using Newtonsoft.Json;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class NewUserSignupViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAccountService AccountService;
        readonly ISetting Settings;
        readonly IDeviceInfo DeviceInfo;
        readonly IUserDialogs Dialog;

        public bool IsAgreementSelected { get; set; }

        public string FirstName { get; set; }
        public bool FirstNameHasError { get; set; }
        public string FirstNameErrorMessage { get; set; }
        public string LastName { get; set; }
        public bool LastNameHasError { get; set; }
        public string LastNameErrorMessage { get; set; }

        public string FullPhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberHasError { get; set; }
        public string PhoneNumberErrorMessage { get; set; }
        public string PhoneExtension { get; set; }
        public bool PhoneExtensionHasError { get; set; }
        public bool PhoneHasError { get { return PhoneNumberHasError || PhoneExtensionHasError; } }

        public string EmailAddress { get; set; }
        public bool EmailAddressHasError { get; set; }
        public string EmailAddressErrorMessage { get; set; }
        public string SecurityQuestion { get; set; }
        public bool SecurityQuestionHasError { get; set; }
        public string SecurityQuestionErrorMessage { get; set; }
        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }
        public string PIN { get; set; }
        public bool PinHasError { get; set; }
        public string PinErrorMessage { get; set; }
        public string ConfirmPIN { get; set; }
        public bool ConfirmPinHasError { get; set; }
        public string ConfirmPinErrorMessage { get; set; }

        //commands
        public Command Register { get; private set; }
        public Command Cancel { get; private set; }
        public Command Agree { get; private set; }

        public NewUserSignupViewModel(IErrorManager ErrorManager, IAppService AppService, IAccountService AccountService, ISetting Settings,
                                        IDeviceInfo DeviceInfo, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager; 
            this.AppService = AppService; 
            this.AccountService = AccountService; 
            this.Settings = Settings; 
            this.DeviceInfo = DeviceInfo;
            this.Dialog = Dialog;

            Register = new Command(async () => await ExecuteRegister());
            Cancel = new Command(async () => await ExecuteCancel());
            Agree = new Command(async () => await ExecuteAgree());
        }

        private async Task ExecuteAgree()
        {
            try
            {
                IsAgreementSelected = !IsAgreementSelected;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteRegister()
        {
            try
            {
                if (!PerformValidation()) return;

                using (Dialog.Loading("Registering..."))
                {
                    FullPhoneNumber = PhoneExtension + PhoneNumber;
                    var request = new SignupRequest
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = EmailAddress,
                        Password = PIN,
                        ConfirmPassword = PIN,
                        PhoneNumber = FullPhoneNumber,
                        SecurityQuestion = SecurityQuestion,
                        SecurityAnswer = SecurityAnswer,
                        MobileDevice = new MobileDevice
                        {
                            DeviceId = DeviceInfo.Id,
                            DeviceType = DeviceInfo.Platform.ToString(),
                            DeviceModel = DeviceInfo.Model,
                            DeviceVersion = DeviceInfo.Version,
                            DeviceVendorId = DeviceInfo.Id
                        }
                    };
                    var response = await AccountService.Signup(request);

                    var nav = new ConfirmAccountViewModel.Nav { Phone = FullPhoneNumber };
                    await CoreMethods.PushPageModel<ConfirmAccountViewModel>(nav, true);
                }
            }
            catch(ApiException ex)
            {
                var errorContent = JsonConvert.DeserializeObject<ErrorMessage>(ex.Content);

                await CoreMethods.DisplayAlert("Error", errorContent?.ModelState?.array?.ToString(), "Ok");

                Debug.WriteLine($"=======ApiException: {ex.Content}=======");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteCancel()
        {
            try
            {
                await CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private bool PerformValidation()
        {
            bool isValid = true;
            bool isValidEmail;

            ClearErrors();

            if (string.IsNullOrEmpty(FirstName))
            {
                FirstNameHasError = true;
                FirstNameErrorMessage = "Please enter your first name.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                LastNameHasError = true;
                LastNameErrorMessage = "Please enter your last name.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(PhoneExtension))
            {
                PhoneExtensionHasError = true;
                PhoneNumberErrorMessage = "Please enter a phone number.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                PhoneNumberHasError = true;
                PhoneNumberErrorMessage = "Please enter a phone number.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(EmailAddress))
            {
                EmailAddressHasError = true;
                EmailAddressErrorMessage = "Please enter an email.";
                isValid = false;
            }

            if (!string.IsNullOrEmpty(EmailAddress))
            {
                isValidEmail = Regex.IsMatch(EmailAddress,
                                         @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                                         RegexOptions.IgnoreCase);
                EmailAddressHasError = !isValidEmail;
                EmailAddressErrorMessage = "Please enter a valid email to continue.";
                isValid = isValidEmail;
            }

            if (string.IsNullOrEmpty(SecurityQuestion))
            {
                SecurityQuestionHasError = true;
                SecurityQuestionErrorMessage = "Please provide a question.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(SecurityAnswer))
            {
                SecurityAnswerHasError = true;
                SecurityAnswerErrorMessage = "Please fill in an answer to the question.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(PIN))
            {
                PinHasError = true;
                PinErrorMessage = "Please enter a password.";
                isValid = false;
            }

            if (PIN != ConfirmPIN)
            {
                PinHasError = true;
                ConfirmPinHasError = true;
                PinErrorMessage = "Both passwords do not match";
                isValid = false;
            }

            return isValid;
        }

        private void ClearErrors()
        {
            FirstNameHasError = LastNameHasError = PhoneExtensionHasError = PhoneNumberHasError = EmailAddressHasError = SecurityQuestionHasError 
            = SecurityAnswerHasError = PinHasError = ConfirmPinHasError = false;
        }
    }
}
