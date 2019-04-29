using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    public class TransactionHistoryDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DepositTemplate { get; set; }
        public DataTemplate RolloverTemplate { get; set; }
        public DataTemplate RedemptionTemplate { get; set; }
        public DataTemplate ConsolidationTemplate { get; set; }
        public TransactionHistoryDataTemplateSelector()
        {

        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate retVal;
            var viewModel = item as TransactionHistoryItemViewModel;

            switch(viewModel?.ActiveTab)
            {
                case TransactionHistoryTab.Deposit:
                    retVal = DepositTemplate;
                    break;
                case TransactionHistoryTab.Rollover:
                    retVal = RolloverTemplate;
                    break;
                case TransactionHistoryTab.Redemption:
                    retVal = RedemptionTemplate;
                    break;
                case TransactionHistoryTab.Consolidation:
                    retVal = ConsolidationTemplate;
                    break;
                default:
                    retVal = DepositTemplate;
                    break;
            }
            return retVal;
        }
    }
}
