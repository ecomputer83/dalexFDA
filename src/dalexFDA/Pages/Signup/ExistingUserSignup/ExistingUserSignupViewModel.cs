using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    [AddINotifyPropertyChangedInterface]
    public class ExistingUserSignupViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;

        public bool IsAgreementSelected { get; set; }

        //commands
        public Command Register { get; private set; }
        public Command Cancel { get; private set; }
        public Command Agree { get; private set; }

        public ExistingUserSignupViewModel(IErrorManager ErrorManager)
        {
            this.ErrorManager = ErrorManager;

            Register = new Command(async () => await ExecuteRegister());
            Cancel = new Command(async () => await ExecuteCancel());
            Agree = new Command(async () => await ExecuteAgree());
        }

        private async Task ExecuteAgree()
        {
            try
            {
                IsAgreementSelected = !IsAgreementSelected;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
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
                await CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}

