using dalexFDA.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    public class TransactionHistoryItemViewModel
    {
        TransactionHistoryViewModel Parent;
        Deposit Deposit;
        Rollover Rollover;
        Redemption Redemption;
        Consolidation Consolidation;
        TransactionHistoryTab ActiveTab;

        public Command ViewDetail { get; private set; }

        public TransactionHistoryItemViewModel(TransactionHistoryViewModel Parent)
        {
            this.Parent = Parent;
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
