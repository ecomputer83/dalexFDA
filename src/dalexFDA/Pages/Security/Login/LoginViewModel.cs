using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using PropertyChanged;
using Acr.UserDialogs;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAuthenticationService AuthService;
        readonly IUserDialogs Dialog;

        //commands
        public Command Login { get; private set; }
        public Command Back { get; private set; }
        public Command SignUpExistingUser { get; private set; }
        public Command SignUpNewUser { get; private set; }
        public Command Validate { get; private set; }

        //properties
        public string AccountNumber { get; set; }
        public string Password { get; set; }

        public string FullPhoneNumber { get { return PhoneExtension?.Replace("+", "") + PhoneNumber; } }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberHasError { get; set; }
        public string PhoneNumberErrorMessage { get; set; }
        public string PhoneExtension { get; set; }
        public bool PhoneExtensionHasError { get; set; }
        public bool PhoneHasError { get { return PhoneNumberHasError || PhoneExtensionHasError; } }

        public string PIN { get; set; }
        public bool PinHasError { get; set; }
        public string PinErrorMessage { get; set; }

        public class CommandNav
        {
            public string Name { get; set; }
        }

        private const string phone_number_error_message = "Please enter a phone number.";
        private const string pin_error_message = "Please enter a PIN.";

        public LoginViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog, IAuthenticationService AuthService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.AuthService = AuthService;

            Login = new Command(async () => await ExecuteLogin());
            Back = new Command(async () => await ExecuteBack());
            SignUpExistingUser = new Command(async () => await ExecuteSignUpExistingUser());
            SignUpNewUser = new Command(async () => await ExecuteSignUpNewUser());
            Validate = new Command<CommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        private async Task ExecuteLogin()
        {
            try
            {
                if (PerformValidation()) return;

                var request = new LoginRequest
                {
                    username = FullPhoneNumber,
                    password = PIN
                };
                var response = await AuthService.Authenticate(request);

                AppService.StartMainFlow();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteBack()
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

        private async Task ExecuteSignUpExistingUser()
        {
            try
            {
                await CoreMethods.PushPageModel<ExistingUserSignupViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteSignUpNewUser()
        {
            try
            {
                await CoreMethods.PushPageModel<NewUserSignupViewModel>();
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

        private void ValidateControls(string name)
        {
            switch (name)
            {
                case "PhoneExtension":
                    PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
                    PhoneNumberErrorMessage = phone_number_error_message;
                    break;
                case "PhoneNumber":
                    PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
                    PhoneNumberErrorMessage = phone_number_error_message;
                    break;
                case "PIN":
                    PinHasError = string.IsNullOrEmpty(PIN);
                    PinErrorMessage = pin_error_message;
                    break;
            }
        }

        private bool PerformValidation()
        {
            PhoneNumberHasError = PhoneExtensionHasError = PinHasError = false;

            PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
            PhoneNumberErrorMessage = phone_number_error_message;
            PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
            PhoneNumberErrorMessage = phone_number_error_message;

            PinHasError = string.IsNullOrEmpty(PIN);
            PinErrorMessage = pin_error_message;

            return PhoneHasError || PinHasError;
        }
    }
}
