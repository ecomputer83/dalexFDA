using System;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    public class DashboardItemViewModel 
    {
        readonly IErrorManager ErrorManager;
        readonly DashboardViewModel Parent;
        public DashboardViewModel.HistoryItem HistoryItem { get; set; }
        public Color AmountColor { get; set; }

        public DashboardItemViewModel(IErrorManager errorManager, DashboardViewModel parent, DashboardViewModel.HistoryItem historyItem)
        {
            ErrorManager = errorManager;
            Parent = parent;
            HistoryItem = historyItem;

            if (HistoryItem.TransactionType == TransactionType.Credit)
                AmountColor = (Color)Application.Current.Resources["Red"];
            else 
                AmountColor = (Color)Application.Current.Resources["Green"];
        }
    }
}
