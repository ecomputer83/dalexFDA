using Acr.UserDialogs;
using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public class ResetPinViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;

        public bool DoesUserExist { get; set; }
        public bool DoesUserNotExist { get { return !DoesUserExist; } }

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

        public string ConfirmPIN { get; set; }
        public bool ConfirmPinHasError { get; set; }
        public string ConfirmPinErrorMessage { get; set; }

        public Command Next { get; private set; }
        public Command Validate { get; private set; }

        private const string phone_number_error_message = "Please enter a phone number.";
        private const string pin_error_message = "Please enter a PIN.";
        private const string confirm_pin_error_message = "Please re-enter your PIN.";
        private const string inconsistent_pin_error_message = "The PINs do not match.";

        public ResetPinViewModel(IErrorManager ErrorManager, IAppService AppService, IUserDialogs Dialog,
                                IAccountService AccountService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.AccountService = AccountService;

            Next = new Command(async () => await ExecuteNext());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteNext()
        {
            try
            {
                if (PerformValidation()) return;

                using (Dialog.Loading("Please wait..."))
                {
                    if (!DoesUserExist)
                    {
                        var user = await AccountService.GetUserByPhoneNumber(FullPhoneNumber);

                        if (user != null)
                            DoesUserExist = true;
                        else
                            await CoreMethods.DisplayAlert("Invalid Phone Number", "The phone number does not exist in our systems.", "Ok");
                    }
                    else
                    {
                        var request = new ResetPinRequest
                        {
                            PhoneNumber = FullPhoneNumber,
                            NewPassword = PIN,
                            ConfirmPassword = PIN
                        };
                        await AccountService.ResetPin(request);

                        var nav = new ConfirmAccountViewModel.Nav { Phone = FullPhoneNumber };
                        await CoreMethods.PushPageModel<ConfirmAccountViewModel>(nav, true);
                    }
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
                case "ConfirmPIN":
                    ConfirmPinHasError = string.IsNullOrEmpty(ConfirmPIN);
                    ConfirmPinErrorMessage = confirm_pin_error_message;
                    break;
            }
        }

        private bool PerformValidation()
        {
            PhoneNumberHasError = PhoneExtensionHasError = PinHasError = ConfirmPinHasError = false;

            if (!DoesUserExist)
            {
                PhoneExtensionHasError = string.IsNullOrEmpty(PhoneExtension);
                PhoneNumberErrorMessage = PhoneExtensionHasError ? phone_number_error_message : "";

                PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber);
                PhoneNumberErrorMessage = PhoneNumberHasError ? phone_number_error_message : "";

                return PhoneHasError;
            }
            else
            {
                PinHasError = string.IsNullOrEmpty(PIN);
                PinErrorMessage = pin_error_message;

                ConfirmPinHasError = string.IsNullOrEmpty(ConfirmPIN);
                ConfirmPinErrorMessage = confirm_pin_error_message;

                if (!PinHasError && !ConfirmPinHasError)
                {
                    PinHasError = ConfirmPinHasError = PIN != ConfirmPIN;
                    PinErrorMessage = PinHasError ? inconsistent_pin_error_message : "";
                    ConfirmPinErrorMessage = "";
                }

                return PinHasError || ConfirmPinHasError;
            }            
        }
    }
}
