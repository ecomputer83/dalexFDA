using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
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
    }
}
