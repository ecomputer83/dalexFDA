using System;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using System.Threading.Tasks;

namespace dalexFDA.Core
{
    public class DashboardItemViewModel 
    {
        readonly IErrorManager ErrorManager;
        readonly DashboardViewModel Parent;
        public InvestmentItem Investment { get; set; }
        public Color AmountColor { get; set; }

        public Command ViewDetail { get; set; }


        public DashboardItemViewModel(IErrorManager errorManager, DashboardViewModel parent, InvestmentItem historyItem)
        {
            ErrorManager = errorManager;
            Parent = parent;
            Investment = historyItem;

            ViewDetail = new Command(async () => await ExecuteViewDetail());
        }

        private async Task ExecuteViewDetail()
        {
            try
            {
                var nav = new InvestmentDetailsViewModel.Nav { Investment = Investment };
                await Parent.CoreMethods.PushPageModel<InvestmentDetailsViewModel>(nav);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
