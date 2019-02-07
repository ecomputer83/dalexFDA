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

        public string Phone { get; set; }
        public string Token { get; set; }
        public bool TokenHasError { get; set; }
        public string TokenErrorMessage { get; set; }

        public Command Confirm { get; private set; }
        public Command Cancel { get; private set; }

        Nav Data;
        public class Nav
        {
            public string Phone { get; set; }
        }

        public ConfirmAccountViewModel(IErrorManager ErrorManager, IAppService AppService, IAccountService AccountService, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            this.AccountService = AccountService;
            this.Dialog = Dialog;

            Confirm = new Command(async () => await ExecuteConfirm());
            Cancel = new Command(async () => await ExecuteCancel());
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
                    TokenErrorMessage = "Please enter the code in the SMS sent to you.";
                    return;
                }


                using (Dialog.Loading("Registering..."))
                {
                    var response = await AccountService.ConfirmAccount(Phone, Token);
                    if (response)
                        AppService.Logout();
                    else
                        await CoreMethods.DisplayAlert("Oops", "The code you entered is incorrect. Please try again", "Ok");
                }
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
    }
}
