using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    [AddINotifyPropertyChangedInterface]
    public class NewUserSignupViewModel : BaseViewModel
    {
        readonly Zenith.Abstractions.IErrorManager ErrorManager;
        readonly Zenith.Abstractions.IAppService AppService;

        public bool IsAgreementSelected { get; set; }

        //commands
        public Command Register { get; private set; }
        public Command Cancel { get; private set; }

        public NewUserSignupViewModel(IErrorManager ErrorManager, IAppService AppService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;

            Register = new Command(async () => await ExecuteRegister());
            Cancel = new Command(async () => await ExecuteCancel());

            IsAgreementSelected = true;
        }

        private async Task ExecuteRegister()
        {
            try
            {

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

            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

    }
}
