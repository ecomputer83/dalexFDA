using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class DepositInvestmentDetailsViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public DateTime TransactionDate { get; set; }

        public Command Negotiate { get; set; }
        public Command Done { get; set; }

        public DepositInvestmentDetailsViewModel(IErrorManager ErrorManager, IAppService AppService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;

            Negotiate = new Command(async () => await ExecuteNegotiate());
            Done = new Command(async () => await ExecuteDone());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {

            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteNegotiate()
        {
            try
            {
                IsSuccessful = true;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteDone()
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
    }
}
