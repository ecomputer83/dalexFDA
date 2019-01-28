using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class RedemptionRequestViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;

        public InvestmentItem Investment { get; set; }

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public Command Negotiate { get; set; }

        Nav Data;
        public class Nav
        {
            public InvestmentItem Investment { get; set; }
        }

        public RedemptionRequestViewModel(IErrorManager ErrorManager)
        {
            this.ErrorManager = ErrorManager;

            Negotiate = new Command(async () => await ExecuteNegotiate());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;
                if (Data != null)
                {
                    Investment = Data.Investment;
                }
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


    }
}
