using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using PropertyChanged;
using Acr.UserDialogs;
using Refit;
using System.Diagnostics;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAuthenticationService AuthService;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;
        readonly ISession SessionService;

        //commands
        public Command Login { get; private set; }
        public Command ResetPin { get; private set; }
        public Command Back { get; private set; }
        public Command SignUpExistingUser { get; private set; }
        public Command SignUpNewUser { get; private set; }
        public Command Validate { get; private set; }

        //properties
        public string AccountNumber { get; set; }
        public string Password { get; set; }

        public string FullPhoneNumber { get { return NumberFormatter.ExtractNumber(PhoneExtension + PhoneNumber); } }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberHasError { get; set; }
        public string PhoneNumberErrorMessage { get; set; }
        public string PhoneExtension { get; set; }
        public bool PhoneExtensionHasError { get; set; }
        public bool PhoneHasError { get { return PhoneNumberHasError || PhoneExtensionHasError; } }

        public string PIN { get; set; }
        public bool PinHasError { get; set; }
        public string PinErrorMessage { get; set; }

        public User User { get; set; }

        public class CommandNav
        {
            public string Name { get; set; }
        }

        private const string phone_number_error_message = "Please enter a phone number.";
        private const string pin_error_message = "Please enter a PIN.";

        public LoginViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog,
            IAuthenticationService AuthService, IAccountService AccountService, ISession SessionService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.AuthService = AuthService;
            this.AccountService = AccountService;
            this.SessionService = SessionService;

            Login = new Command(async () => await ExecuteLogin());
            ResetPin = new Command(async () => await ExecuteResetPin());
            Back = new Command(async () => await ExecuteBack());
            SignUpExistingUser = new Command(async () => await ExecuteSignUpExistingUser());
            SignUpNewUser = new Command(async () => await ExecuteSignUpNewUser());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                //PhoneExtension = "+234";
                //PhoneNumber = "7037509734";
                //PIN = "1234";
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteLogin()
        {
            try
            {
                if (PerformValidation()) return;

                using (Dialog.Loading("Authenticating..."))
                {
                    var request = new LoginRequest
                    {
                        username = FullPhoneNumber,
                        password = PIN
                    };
                    var response = await AuthService.Authenticate(request);

                    if (response != null)
                    {
                        SessionService.Token = response.access_token;

                        var user = await AccountService.GetUser();
                        if (user != null)
                        {
                            SessionService.CurrentUser = user;
                            AppService.StartMainFlow();
                        }
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Oops", "Invalid Phone number or password. Please try again.", "Ok");
                    }
                }
            }
            catch(ApiException ex)
            {
                await CoreMethods.DisplayAlert("Oops", "Invalid Phone number or password. Please try again.", "Ok");
                Debug.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Oops", "Invalid Phone number or password. Please try again.", "Ok");
                Debug.WriteLine($"{ex.Message}");
            }
        }

        private async Task ExecuteResetPin()
        {
            try
            {
                await CoreMethods.PushPageModel<ResetPinViewModel>();
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
                case "PhoneExtension":
                    PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
                    PhoneNumberErrorMessage = PhoneExtensionHasError ? phone_number_error_message : "";
                    break;
                case "PhoneNumber":
                    PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
                    PhoneNumberErrorMessage = PhoneNumberHasError ? phone_number_error_message : "";
                    break;
                case "PIN":
                    PinHasError = string.IsNullOrEmpty(PIN);
                    PinErrorMessage = PinHasError ? pin_error_message : "";
                    break;
            }
        }

        private bool PerformValidation()
        {
            PhoneNumberHasError = PhoneExtensionHasError = PinHasError = false;

            PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
            PhoneNumberErrorMessage = PhoneExtensionHasError ? phone_number_error_message : "";

            PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
            PhoneNumberErrorMessage = PhoneNumberHasError ? phone_number_error_message : "";

            PinHasError = string.IsNullOrEmpty(PIN);
            PinErrorMessage = PinHasError ? pin_error_message : "";

            return PhoneHasError || PinHasError;
        }
    }
}
