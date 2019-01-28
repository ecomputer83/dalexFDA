using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class CardPaymentDetailsViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;

        public Command Pay { get; private set; }

        public CardPaymentDetailsViewModel(IErrorManager ErrorManager)
        {
            this.ErrorManager = ErrorManager;

            Pay = new Command(async () => await ExecutePay());
        }

        private async Task ExecutePay()
        {
            try
            {
                await CoreMethods.PushPageModel<DepositInvestmentDetailsViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
