using System;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    public class DashboardItemViewModel 
    {
        readonly IErrorManager ErrorManager;
        readonly DashboardViewModel Parent;
        public DashboardViewModel.InvestmentItem Investment { get; set; }
        public Color AmountColor { get; set; }

        public DashboardItemViewModel(IErrorManager errorManager, DashboardViewModel parent, DashboardViewModel.InvestmentItem historyItem)
        {
            ErrorManager = errorManager;
            Parent = parent;
            Investment = historyItem;
        }
    }
}
