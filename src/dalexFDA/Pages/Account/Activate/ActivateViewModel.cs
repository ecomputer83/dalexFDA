using System;
using System.Threading.Tasks;

namespace  Zenith
{
    public partial class ActivateViewModel: BaseViewModel
    {


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

        private async Task ExecuteDebitCard()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("DebitCard", "DebitCard", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteHardwareToken()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("HardwareToken", "HardwareToken", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteOpenAccount()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("OpenAccount", "OpenAccount", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteQuickAccess()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("QuickAccess", "QuickAccess", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteRegisterNewDevice()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("RegisterNewDevice", "RegisterNewDevice", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteTermsAndConditions()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("TermsAndConditions", "TermsAndConditions", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}

