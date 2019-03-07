using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class ConfirmAccountViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;
        readonly ISession SessionService;

        public string Phone { get; set; }
        public string Token { get; set; }
        public bool TokenHasError { get; set; }
        public string TokenErrorMessage { get; set; }

        public Command Confirm { get; private set; }
        public Command Cancel { get; private set; }
        public Command Validate { get; private set; }

        Nav Data;
        public class Nav
        {
            public string Phone { get; set; }
            public string type { get; set; }
        }

        private const string token_error_message = "Please enter the code in the SMS sent to you.";

        public ConfirmAccountViewModel(IErrorManager ErrorManager, IAppService AppService, IAccountService AccountService, IUserDialogs Dialog, ISession SessionService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.AccountService = AccountService;
            this.Dialog = Dialog;
            this.SessionService = SessionService;

            Confirm = new Command(async () => await ExecuteConfirm());
            Cancel = new Command(async () => await ExecuteCancel());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;

                if (Data != null)
                {
                    Phone = Data.Phone;
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteConfirm()
        {
            try
            {
                TokenHasError = false;

                if (string.IsNullOrEmpty(Token))
                {
                    TokenHasError = true;
                    TokenErrorMessage = token_error_message;
                    return;
                }

                using (Dialog.Loading("Verifying..."))
                {
                    var response = await AccountService.ConfirmAccount(Phone, Token);

                    if (response)
                    {
                        if (string.IsNullOrEmpty(Data.type))
                        {
                            AppService.Logout();
                        }
                        else
                        {
                            var user = await AccountService.GetUser();
                            if (user != null)
                            {
                                SessionService.CurrentUser = user;
                                AppService.StartMainFlow();
                            }
                        }
                    }
                    else
                        await CoreMethods.DisplayAlert("Oops", "The code you entered is incorrect. Please try again", "Ok");
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

        private async Task ExecuteCancel()
        {
            try
            {
                AppService.Logout();
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
                case "Token":
                    TokenHasError = string.IsNullOrEmpty(Token);
                    TokenErrorMessage = TokenHasError ? token_error_message : "";
                    break;
            }
        }
    }
}
