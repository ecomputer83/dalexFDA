using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    public class TransactionHistoryItemViewModel
    {
        TransferViewModel Parent;

        public Command ViewDetail { get; private set; }

        public TransactionHistoryItemViewModel(TransferViewModel parent)
        {
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
