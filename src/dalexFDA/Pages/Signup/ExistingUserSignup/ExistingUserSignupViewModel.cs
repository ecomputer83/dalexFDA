using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using Plugin.DeviceInfo.Abstractions;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using Refit;
using Newtonsoft.Json;
using System.Diagnostics;
using Plugin.Connectivity.Abstractions;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class ExistingUserSignupViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAccountService AccountService;
        readonly ISetting Settings;
        readonly IDeviceInfo DeviceInfo;
        readonly IUserDialogs Dialog;
        readonly ILookupService LookupService;
        readonly ISession session;
        readonly IConnectivity Connectivity;

        public bool IsAgreementSelected { get; set; }
        public bool IsRegisterEnabled { get { return !IsAgreementSelected; } }
        public Style RegisterButtonStyle { get { return IsAgreementSelected ? (Style)Application.Current.Resources["PrimaryButton"]
                                                                        : (Style)Application.Current.Resources["DisabledButton"]; } }

        public string FullName { get; set; }
        public bool FullNameHasError { get; set; }
        public string FullNameErrorMessage { get; set; }

        public string FullPhoneNumber { get { return NumberFormatter.ExtractNumber(PhoneExtension + PhoneNumber); } }
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
        public string SecurityHint { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }
        public string PIN { get; set; }
        public bool PinHasError { get; set; }
        public string PinErrorMessage { get; set; }
        public string ConfirmPIN { get; set; }
        public bool ConfirmPinHasError { get; set; }
        public string ConfirmPinErrorMessage { get; set; }

        public string TermsText { get; set; }
        public string PrivacyPolicyText { get; set; }

        public User User { get; set; }

        //commands
        public Command Register { get; private set; }
        public Command GetUserDetailsFromPhoneNumber { get; private set; }
        public Command Cancel { get; private set; }
        public Command Agree { get; private set; }
        public Command Validate { get; private set; }
        public Command Terms { get; private set; }
        public Command PrivacyPolicy { get; private set; }

        private const string fullname_error_message = "Your fullname is required.";
        private const string phone_number_error_message = "Please enter a phone number.";
        private const string email_address_error_message = "Please enter an email address.";
        private const string invalid_email_address_error_message = "Please enter a valid email address.";
        private const string security_question_error_message = "Your security question is required.";
        private const string security_answer_error_message = "Please provide an answer to the question.";
        private const string incorrect_security_answer_error_message = "The answer you provided is incorrect.";
        private const string pin_error_message = "Please enter a PIN.";
        private const string confirmpin_error_message = "Please re-enter your PIN.";
        private const string inconsistent_pin_error_message = "The PINs do not match.";
        private const string empty_string = "";

        public ExistingUserSignupViewModel(IErrorManager ErrorManager, IAppService AppService, IAccountService AccountService, ISetting Settings, ISession session,
                                        IDeviceInfo DeviceInfo, IUserDialogs Dialog, ILookupService LookupService, IConnectivity connectivity)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.AccountService = AccountService;
            this.Settings = Settings;
            this.DeviceInfo = DeviceInfo;
            this.Dialog = Dialog;
            this.LookupService = LookupService;
            this.session = session;
            this.Connectivity = connectivity;

            Register = new Command(async () => await ExecuteRegister());
            Cancel = new Command(async () => await ExecuteCancel());
            Agree = new Command(async () => await ExecuteAgree());
            GetUserDetailsFromPhoneNumber = new Command(async () => await ExecuteGetUserDetailsFromPhoneNumber());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
            Terms = new Command(async () => await ExecuteTerms());
            PrivacyPolicy = new Command(async () => await ExecutePrivacyPolicy());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading())
                {
                    TermsText = await LookupService.GetTermsAndConditions();
                    PrivacyPolicyText = await LookupService.GetPrivacyPolicy();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
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

        private async Task ExecuteGetUserDetailsFromPhoneNumber()
        {
            try
            {
                if (!string.IsNullOrEmpty(PhoneExtension) && !string.IsNullOrEmpty(PhoneNumber))
                {
                    using (Dialog.Loading("Fetching details..."))
                    {
                        var phoneExtension = NumberFormatter.ExtractNumber(PhoneExtension);
                        var phoneNumber = NumberFormatter.ExtractNumber(PhoneNumber);
                        User = await AccountService.GetKYCAccountByPhoneNumber(phoneExtension, phoneNumber);

                        if (User != null)
                        {
                            FullName = User.Name;
                            EmailAddress = User.Email;
                            SecurityQuestion = User.SecurityQuestion;
                            SecurityHint = User.SecurityHint;
                        }
                        else
                        {
                            FullName = EmailAddress = SecurityQuestion = "";
                        }
                    }
                }
            }
            catch (ApiException ex)
            {
                await CoreMethods.DisplayAlert("Oops", "An error occured. Please try again later.", "Ok");
                Debug.WriteLine($"=======ApiException: {ex.Content}=======");
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

        private async Task ExecuteRegister()
        {
            try
            {
                if (!IsAgreementSelected) return;
                if (PerformValidation()) return;
                if (!Connectivity.IsConnected)
                {
                    throw new Exception("No internet connection, Please connect to internet");
                }

                using (Dialog.Loading("Please wait..."))
                {
                    var request = new SignupRequest
                    {
                        Name = FullName,
                        Email = EmailAddress,
                        Password = PIN,
                        ConfirmPassword = PIN,
                        PhoneNumber = NumberFormatter.ExtractNumber(PhoneNumber),
                        Ext = NumberFormatter.ExtractNumber(PhoneExtension),
                        SecurityQuestion = SecurityQuestion,
                        SecurityAnswer = SecurityAnswer,
                        SecurityHint = SecurityHint,
                        MobileDevice = new MobileDevice
                        {
                            DeviceId = this.DeviceInfo.Id,
                            DeviceType = DeviceInfo.Platform.ToString(),
                            DeviceVersion = DeviceInfo.Version,
                            DeviceVendorId = DeviceInfo.Id,
                            DeviceModel = DeviceInfo.Model,
                            PushNotificationId = this.session?.PushNotification?.PushNotificationID,
                            PushNotificationAppId = this.session?.PushNotification?.PushNotificationAppID,
                            PushNotificationService = this.session?.PushNotification?.PushNotificationService
                        }

                    };
                    var response = await AccountService.Signup(request);

                    var nav = new ConfirmAccountViewModel.Nav { Phone = FullPhoneNumber };
                    await CoreMethods.PushPageModel<ConfirmAccountViewModel>(nav, true);
                }
            }
            catch (ApiException ex)
            {
                var errorContent = JsonConvert.DeserializeObject<ErrorMessage>(ex.Content);

                await CoreMethods.DisplayAlert("Oops", "An error occured. Please try again later.", "Ok");

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

        private async Task ExecuteTerms()
        {
            try
            {
                var nav = new MoreDetailsViewModel.Nav { Message = TermsText, Title = "Terms and Conditions" };
                await CoreMethods.PushPageModel<MoreDetailsViewModel>(nav, true);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecutePrivacyPolicy()
        {
            try
            {
                var nav = new MoreDetailsViewModel.Nav { Message = PrivacyPolicyText, Title = "Privacy Policy" };
                await CoreMethods.PushPageModel<MoreDetailsViewModel>(nav, true);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private void ValidateControls(string name)
        {
            switch(name)
            {
                case "PhoneExtension":
                    PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
                    PhoneNumberErrorMessage = PhoneExtensionHasError ? phone_number_error_message : empty_string;
                    break;
                case "PhoneNumber":
                    PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
                    PhoneNumberErrorMessage = PhoneNumberHasError ? phone_number_error_message : empty_string;
                    break;
                case "FullName":
                    FullNameHasError = string.IsNullOrEmpty(FullName);
                    FullNameErrorMessage = FullNameHasError ? fullname_error_message : empty_string;
                    break;
                case "EmailAddress":
                    EmailAddressHasError = string.IsNullOrEmpty(EmailAddress);
                    EmailAddressErrorMessage = email_address_error_message;
                    EmailAddressHasError = !EmailAddressHasError ? !ValidateEmail(EmailAddress) : EmailAddressHasError;
                    EmailAddressErrorMessage = !EmailAddressHasError && !ValidateEmail(EmailAddress) ? "" : invalid_email_address_error_message;
                    break;
                case "SecurityQuestion":
                    SecurityQuestionHasError = string.IsNullOrEmpty(SecurityQuestion);
                    SecurityQuestionErrorMessage = SecurityQuestionHasError ? security_question_error_message : empty_string;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = SecurityAnswerHasError ? security_answer_error_message : empty_string;
                    break;
                case "PIN":
                    PinHasError = string.IsNullOrEmpty(PIN);
                    PinErrorMessage = PinHasError ? pin_error_message : empty_string;
                    break;
                case "ConfirmPIN":
                    ConfirmPinHasError = string.IsNullOrEmpty(ConfirmPIN);
                    ConfirmPinErrorMessage = ConfirmPinHasError ? confirmpin_error_message : empty_string;
                    break;
                default:
                    break;
            }
        }

        private bool PerformValidation()
        {
            ClearErrors();
            bool isValidEmail = !string.IsNullOrEmpty(EmailAddress) ? ValidateEmail(EmailAddress) : false;

            PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
            PhoneNumberErrorMessage = PhoneExtensionHasError ? phone_number_error_message : empty_string;
            PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
            PhoneNumberErrorMessage = PhoneNumberHasError ? phone_number_error_message : empty_string;

            FullNameHasError = string.IsNullOrEmpty(FullName);
            FullNameErrorMessage = FullNameHasError ? fullname_error_message : empty_string;

            EmailAddressHasError = string.IsNullOrEmpty(EmailAddress);
            EmailAddressErrorMessage = email_address_error_message;
            EmailAddressHasError = !EmailAddressHasError ? !isValidEmail : EmailAddressHasError;
            EmailAddressErrorMessage = !EmailAddressHasError && !isValidEmail ? empty_string : invalid_email_address_error_message;

            SecurityQuestionHasError = string.IsNullOrEmpty(SecurityQuestion);
            SecurityQuestionErrorMessage = SecurityQuestionHasError ? security_question_error_message : empty_string;
            SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
            SecurityAnswerErrorMessage = SecurityAnswerHasError ? security_answer_error_message : empty_string;

            PinHasError = string.IsNullOrEmpty(PIN);
            PinErrorMessage = PinHasError ? pin_error_message : empty_string;
            ConfirmPinHasError = string.IsNullOrEmpty(ConfirmPIN);
            ConfirmPinErrorMessage = ConfirmPinHasError ? confirmpin_error_message : empty_string;
            if (!PinHasError && !ConfirmPinHasError)
            {
                PinHasError = ConfirmPinHasError = PIN != ConfirmPIN;
                PinErrorMessage = PinHasError ? inconsistent_pin_error_message : empty_string;
                ConfirmPinErrorMessage = empty_string;
            }

            return PhoneHasError || FullNameHasError || EmailAddressHasError || SecurityQuestionHasError || SecurityAnswerHasError ||
                PinHasError || ConfirmPinHasError;
        }

        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email,
                                         @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                                         RegexOptions.IgnoreCase);
        }

        private void ClearErrors()
        {
            FullNameHasError = PhoneExtensionHasError = PhoneNumberHasError = EmailAddressHasError = SecurityQuestionHasError
            = SecurityAnswerHasError = PinHasError = ConfirmPinHasError = false;
        }
    }
}

