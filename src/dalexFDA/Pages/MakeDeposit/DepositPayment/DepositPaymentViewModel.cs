using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    [AddINotifyPropertyChangedInterface]
    public class DepositPaymentViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;

        public bool IsBank { get; set; }
        public bool IsCard { get { return !IsBank; } }

        //commands
        public Command Bank { get; private set; }
        public Command Card { get; private set; }

        public DepositPaymentViewModel(IErrorManager ErrorManager)
        {
            this.ErrorManager = ErrorManager;

            Bank = new Command(async () => await ExecuteBank());
            Card = new Command(async () => await ExecuteCard());

            IsBank = true;
        }

        private async Task ExecuteBank()
        {
            try
            {
                IsBank = true;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteCard()
        {
            try
            {
                IsBank = false;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
