using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace  Zenith
{
    public class CategoricalData
    {
        public object Category { get; set; }
        public double Value { get; set; }
    }

    public class MonthDemand
    {

        public MonthDemand(string text, double value)
        {
            this.Value = value;

            this.Text = text;
        }

        public string Text { get; set; }

        public double Value { get; set; }

    }

    public partial class DashboardViewModel: BaseViewModel
    {
        public ObservableCollection<DashboardItemViewModel> HistoryItemsSource { get; set; }
        public List<InvestmentItem> Investments { get; set; } 

            
        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading())
                {
                    LastSession = DateTime.Now.ToUniversalTime().ToString();
                    IsOverviewTab = true;
                    SetupTransactions();
                    SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        public async void SetupTransactions()
        {
            TotalAmount = "GHC 999,999,000.00";
        }

        public void SetupHistoryItems()
        {
            var list = new List<DashboardItemViewModel>();
            Investments = new List<InvestmentItem>
            {
                new InvestmentItem { Id = "INV00019", StartDate = DateTime.Now.AddDays(-20), Principal = "GHS 1,000,000.00", Days = "380", Rate = "23% p.a", Maturity="GHS 1,000,000.00" },
                new InvestmentItem { Id = "INV00020", StartDate = DateTime.Now.AddDays(-50), Principal = "GHS 10,000,000.00", Days = "380", Rate = "3.75% p.a", Maturity="GHS 10,000,000.00" },
                new InvestmentItem { Id = "INV00021", StartDate = DateTime.Now.AddDays(-70), Principal = "GHS 1,500,000.00", Days = "380", Rate = "3.75% p.a", Maturity="GHS 1,500,000.00" },
                new InvestmentItem { Id = "INV00022", StartDate = DateTime.Now.AddDays(-90), Principal = "GHS 1,700,000.00", Days = "380", Rate = "3.75% p.a", Maturity="GHS 1,700,000.00" },
                new InvestmentItem { Id = "INV00023", StartDate = DateTime.Now.AddDays(-120), Principal = "GHS 15,000,000.00", Days = "380", Rate = "12.75% p.a", Maturity="GHS 15,000,000.00" }
            };

            foreach(var item in Investments)
            {
                list.Add(new DashboardItemViewModel(ErrorManager, this, item));
            }
            HistoryItemsSource = new ObservableCollection<DashboardItemViewModel>(list);
        }

        public class InvestmentItem
        {
            public string Id { get; set; }
            public DateTime StartDate { get; set; }
            public string Principal { get; set; }
            public string Days { get; set; }
            public string Rate { get; set; }
            public string Maturity { get; set; }
        }
    }
}

