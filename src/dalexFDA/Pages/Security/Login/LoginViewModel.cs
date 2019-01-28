using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;

namespace dalexFDA
{
    public partial class LoginViewModel : BaseViewModel
    {
        private async Task ExecuteLogin()
        {
            try
            {
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
    }
}
