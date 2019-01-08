using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    public partial class AuthenticationWelcomeViewModel : BaseViewModel
    {

        private async Task ExecuteLogin() 
        {
            try
            {
                await CoreMethods.PushPageModel<LoginViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteActivate()
        {
            try
            {
                await CoreMethods.PushPageModel<ActivateViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}

