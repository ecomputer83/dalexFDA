using System;
using System.Threading.Tasks;

namespace  Zenith
{
    public partial class LeftNavViewModel: BaseViewModel
    {
        private async Task ExecuteOverview()
        {
            try
            {
                AppService.StartOverviewFlow();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteTransfer()
        {
            try
            {
                AppService.StartTransferFlow();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteLogout()
        {
            try
            {
                var title = "LOG OUT?";
                var message = "Are you sure you want to log out?";
                var cancelText = "Cancel";
                var okText = "Yes";

                var okTextSelected = await Dialog.ConfirmAsync(message, title, okText, cancelText);

                if (okTextSelected)
                {
                    AppService.Logout();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}

