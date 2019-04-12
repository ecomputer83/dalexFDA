using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using PropertyChanged;
using Acr.UserDialogs;
using Refit;
using System.Diagnostics;
using Plugin.Connectivity.Abstractions;
using Plugin.DeviceInfo.Abstractions;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel : BaseViewModel
    {
        readonly IConnectivity Connectivity;
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAuthenticationService AuthService;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;
        readonly ISession SessionService;
        readonly ISetting SettingService;
        readonly IDeviceInfo deviceInfo;

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
        public bool isConnected { get; set; }

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

        private const string phone_number_error_message = "Phone number should not start with 0, Field required.";
        private const string pin_error_message = "Please enter a PIN.";

        public LoginViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog, IDeviceInfo DeviceInfo,
            IAuthenticationService AuthService, IAccountService AccountService, ISession SessionService, ISetting SettingService, IConnectivity connectivity)
        {
            this.Connectivity = connectivity;
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.AuthService = AuthService;
            this.AccountService = AccountService;
            this.SessionService = SessionService;
            this.SettingService = SettingService;
            this.deviceInfo = DeviceInfo;

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

                isConnected = Connectivity.IsConnected;
                if (!Connectivity.IsConnected)
                {
                    throw new Exception("No internet connection, Please connect to internet");
                }

                App app = Application.Current as App;
                if (app.RegisterPushNotificationService != null)
                {
                    app.RegisterPushNotificationService();
                }
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
                if (!Connectivity.IsConnected)
                {
                    throw new Exception("No internet connection, Please connect to internet");
                }
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

                        var device = await AccountService.GetDevice();

                        if ((response.PhoneConfirmation == "0" && response.EmailConfirmation == "0") || device.DeviceId != deviceInfo.Id)
                        {
                            if(device.DeviceId != deviceInfo.Id)
                            {
                                await AccountService.GenerateSMSToken(FullPhoneNumber);
                            }
                            var nav = new ConfirmAccountViewModel.Nav { Phone = FullPhoneNumber, type = "isToken" };
                            await CoreMethods.PushPageModel<ConfirmAccountViewModel>(nav, true);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(SettingService?.PushNotificationAppID))
                            {
                                device.PushNotificationAppId = SettingService?.PushNotificationAppID;
                                device.PushNotificationId = SettingService?.PushNotificationID;
                                device.PushNotificationService = SettingService?.PushNotificationService;

                                await AccountService.UpdateMobileDevice(device);
                            }
                            var user = await AccountService.GetUser();
                            if (user != null)
                            {
                                SessionService.CurrentUser = user;
                                SettingService.isFirstTime = false;

                                if (user.Status == UserAccountStatus.Active)
                                {
                                    AppService.StartMainFlow();
                                }
                                else
                                {
                                    AppService.StartKYCUpdate();
                                }
                            }
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
                Crashes.TrackError(ex);
                var content = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(ex.Content);
                if (content != null)
                {
                    if (content.error != null)
                    {
                        if (content.error.error_description != null)
                        {
                            await CoreMethods.DisplayAlert("Oops", content.error.error_description, "Ok");
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Oops", content.error.ToString(), "Ok");
                        }
                    }
                    else if (content.ModelState != null)
                    {
                        if (content.ModelState.error != null)
                        {
                            await CoreMethods.DisplayAlert("Oops", string.Join(", ", content.ModelState.error), "Ok");
                        }
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Oops", ex.Message, "Ok");
                    }
                }
                else
                {
                    await CoreMethods.DisplayAlert("Oops", "error 001 - An error occured, kindly contact administrator.", "Ok");
                }

                Debug.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex);

                await CoreMethods.DisplayAlert("Oops", "error "+ex.Message+" - An error occured, kindly contact administrator.", "Ok");
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
                    PhoneNumberHasError = !string.IsNullOrEmpty(PhoneNumber) ? PhoneNumber.StartsWith("0") : true;
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
