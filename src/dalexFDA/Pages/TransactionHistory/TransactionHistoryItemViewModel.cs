using dalexFDA.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    public class TransactionHistoryItemViewModel
    {
        TransactionHistoryViewModel Parent;
        public Deposit Deposit { get; set; }
        public Rollover Rollover { get; set; }
        public Redemption Redemption { get; set; }
        public Consolidation Consolidation { get; set; }
        public TransactionHistoryTab ActiveTab;

        public Command ViewDetail { get; private set; }

        public TransactionHistoryItemViewModel(TransactionHistoryViewModel Parent, object item, TransactionHistoryTab activeTab)
        {
            this.Parent = Parent;

            switch (activeTab)
            {
                case TransactionHistoryTab.Deposit:
                    Deposit = item as Deposit;
                    break;
                case TransactionHistoryTab.Rollover:
                    Rollover = item as Rollover;
                    break;
                case TransactionHistoryTab.Redemption:
                    Redemption = item as Redemption;
                    break;
                case TransactionHistoryTab.Consolidation:
                    Consolidation = item as Consolidation;
                    break;
                default:
                    Deposit = item as Deposit;
                    break;
            }
        }

        private async Task ExecuteViewDetail()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await Parent.ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
