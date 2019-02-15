﻿using System;
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

        public bool IsAgreementSelected { get; set; }
        public bool IsRegisterEnabled { get { return !IsAgreementSelected; } }
        public Style RegisterButtonStyle { get { return IsAgreementSelected ? (Style)Application.Current.Resources["PrimaryButton"]
                                                                        : (Style)Application.Current.Resources["DisabledButton"]; } }

        public string FullName { get; set; }
        public bool FullNameHasError { get; set; }
        public string FullNameErrorMessage { get; set; }

        public string FullPhoneNumber { get { return PhoneExtension?.Replace("+", "") + PhoneNumber; } }
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

        public User User { get; set; }

        //commands
        public Command Register { get; private set; }
        public Command GetUserDetailsFromPhoneNumber { get; private set; }
        public Command Cancel { get; private set; }
        public Command Agree { get; private set; }
        public Command Validate { get; private set; }

        public class CommandNav
        {
            public string Name { get; set; }
        }

        private const string phone_number_error_message = "Please enter a phone number.";
        private const string email_address_error_message = "Please enter an email address.";
        private const string invalid_email_address_error_message = "Please enter a valid email address.";
        private const string security_answer_error_message = "Please provide an answer to the question.";
        private const string incorrect_security_answer_error_message = "The answer you provided is incorrect.";
        private const string pin_error_message = "Please enter a PIN.";
        private const string confirmpin_error_message = "Please re-enter your PIN.";
        private const string inconsistent_pin_error_message = "The PINs do not match.";

        public ExistingUserSignupViewModel(IErrorManager ErrorManager, IAppService AppService, IAccountService AccountService, ISetting Settings,
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
            GetUserDetailsFromPhoneNumber = new Command(async () => await ExecuteGetUserDetailsFromPhoneNumber());
            Validate = new Command<CommandNav>(async (obj) => await ExecuteValidate(obj));
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
                        User = await AccountService.GetUserByPhoneNumber(FullPhoneNumber);

                        if (User != null)
                        {
                            FullName = User.Name;
                            EmailAddress = User.Email;
                            SecurityQuestion = User.SecurityQuestion;
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

        private async Task ExecuteValidate(CommandNav obj)
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

                using (Dialog.Loading("Registering..."))
                {
                    var request = new SignupRequest
                    {
                        Name = FullName,
                        Email = EmailAddress,
                        Password = PIN,
                        ConfirmPassword = PIN,
                        PhoneNumber = FullPhoneNumber,
                        SecurityQuestion = SecurityQuestion,
                        SecurityAnswer = SecurityAnswer
                    };
                    var response = await AccountService.SignupExistingUser(request);

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

        private void ValidateControls(string name)
        {
            switch(name)
            {
                case "PhoneExtension":
                    PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
                    PhoneNumberErrorMessage = phone_number_error_message;
                    break;
                case "PhoneNumber":
                    PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
                    PhoneNumberErrorMessage = phone_number_error_message;
                    break;
                case "FullName":
                    FullNameHasError = string.IsNullOrEmpty(FullName);
                    break;
                case "EmailAddress":
                    EmailAddressHasError = string.IsNullOrEmpty(EmailAddress);
                    EmailAddressErrorMessage = email_address_error_message;
                    EmailAddressHasError = !EmailAddressHasError ? !ValidateEmail(EmailAddress) : EmailAddressHasError;
                    EmailAddressErrorMessage = !EmailAddressHasError && !ValidateEmail(EmailAddress) ? "" : invalid_email_address_error_message;
                    break;
                case "SecurityQuestion":
                    SecurityQuestionHasError = string.IsNullOrEmpty(SecurityQuestion);
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
                case "PIN":
                    PinHasError = string.IsNullOrEmpty(PIN);
                    PinErrorMessage = pin_error_message;
                    break;
                case "ConfirmPIN":
                    ConfirmPinHasError = string.IsNullOrEmpty(ConfirmPIN);
                    ConfirmPinErrorMessage = confirmpin_error_message;
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
            PhoneNumberErrorMessage = phone_number_error_message;
            PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
            PhoneNumberErrorMessage = phone_number_error_message;

            FullNameHasError = string.IsNullOrEmpty(FullName);

            EmailAddressHasError = string.IsNullOrEmpty(EmailAddress);
            EmailAddressErrorMessage = email_address_error_message;
            EmailAddressHasError = !EmailAddressHasError ? !isValidEmail : EmailAddressHasError;
            EmailAddressErrorMessage = !EmailAddressHasError && !isValidEmail ? "" : invalid_email_address_error_message;

            SecurityQuestionHasError = string.IsNullOrEmpty(SecurityQuestion);
            SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
            SecurityAnswerErrorMessage = security_answer_error_message;

            PinHasError = string.IsNullOrEmpty(PIN);
            PinErrorMessage = pin_error_message;
            ConfirmPinHasError = string.IsNullOrEmpty(ConfirmPIN);
            ConfirmPinErrorMessage = confirmpin_error_message;
            if(!PinHasError && !ConfirmPinHasError)
            {
                PinHasError = ConfirmPinHasError = PIN != ConfirmPIN;
                PinErrorMessage = inconsistent_pin_error_message;
                ConfirmPinErrorMessage = string.Empty;
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

