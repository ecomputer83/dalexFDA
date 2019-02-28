using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class MyProfileViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;

        public User Profile { get; set; }
        public string DisplayName { get; set; }

        public Command ChangePhoto { get; set; }
         
        public MyProfileViewModel(IErrorManager ErrorManager, IAccountService AccountService, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.AccountService = AccountService;
            this.Dialog = Dialog;

            ChangePhoto = new Command(async () => await ExecuteChangePhoto());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading("Gettng info..."))
                {
                    Profile = await AccountService.GetUser();
                    DisplayName = Profile != null ? Profile.Name : "";
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteChangePhoto()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
