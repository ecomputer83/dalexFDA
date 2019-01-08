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
        public ObservableCollection<CategoricalData> Data { get; set; }
        public ObservableCollection<CategoricalData> Transactions { get; set; }
        public ObservableCollection<DashboardAccountItem> DashboardAccountItems { get; set; }
        public ObservableCollection<DashboardItemViewModel> HistoryItemsSource { get; set; }
        public List<HistoryItem> HistoryItems { get; set; } 
        public bool IsHistoryTab { get { return !IsOverviewTab; }}
        public Style OverviewTabStyle { get; set; }
        public Style HistoryTabStyle { get; set; }

        private Style activeTab = (Style)Application.Current.Resources["ActiveTab"];
        private Style inactiveTab = (Style)Application.Current.Resources["InactiveTab"];
            
        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading())
                {
                    LastSession = DateTime.Now.ToUniversalTime().ToString();
                    IsOverviewTab = true;
                    SetupDashboardItems();
                    SetupTransactions(0);
                    SetupHistoryItems(0);
                    OverviewTabStyle = activeTab;
                    HistoryTabStyle = inactiveTab;
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private static ObservableCollection<CategoricalData> GetCategoricalData()
        {
            var data = new ObservableCollection<CategoricalData>  {
            new CategoricalData { Category = "A", Value = 0.63 },
            new CategoricalData { Category = "B", Value = 0.85 },
            new CategoricalData { Category = "C", Value = 1.05 },
            new CategoricalData { Category = "D", Value = 0.96 },
            new CategoricalData { Category = "E", Value = 0.78 },
        };

            return data;
        }

        private void SetupDashboardItems()
        {
            DashboardAccountItems = new ObservableCollection<DashboardAccountItem>
            {
                new DashboardAccountItem { AccountNumber="1234515612", AccountType = "CURRENT ACCOUNT" },
                new DashboardAccountItem { AccountNumber="4714123411", AccountType = "SAVINGS ACCOUNT" }
            };
        }

        public async void SetupTransactions(int position)
        {
            try
            {
                Transactions = new ObservableCollection<CategoricalData>();
                switch (position)
                {
                    case 0:
                        Transactions.Add(new CategoricalData { Category = "Jan", Value = 44 });
                        Transactions.Add(new CategoricalData { Category = "Feb", Value = 53 });
                        Transactions.Add(new CategoricalData { Category = "Mar", Value = 64 });
                        Transactions.Add(new CategoricalData { Category = "Apr", Value = 75 });
                        Transactions.Add(new CategoricalData { Category = "May", Value = 83 });
                        Transactions.Add(new CategoricalData { Category = "Jun", Value = 87 });
                        Transactions.Add(new CategoricalData { Category = "Jul", Value = 84 });
                        Transactions.Add(new CategoricalData { Category = "Sep", Value = 78 });
                        Transactions.Add(new CategoricalData { Category = "Oct", Value = 67 });
                        Transactions.Add(new CategoricalData { Category = "Nov", Value = 55 });
                        Transactions.Add(new CategoricalData { Category = "Dec", Value = 45 });
                        TotalAmount = "GHS 999,999,000.00";
                        break;
                    case 1:
                        Transactions.Add(new CategoricalData { Category = "Jan", Value = 61 });
                        Transactions.Add(new CategoricalData { Category = "Feb", Value = 46 });
                        Transactions.Add(new CategoricalData { Category = "Mar", Value = 66 });
                        Transactions.Add(new CategoricalData { Category = "Apr", Value = 09 });
                        Transactions.Add(new CategoricalData { Category = "May", Value = 90 });
                        Transactions.Add(new CategoricalData { Category = "Jun", Value = 45 });
                        Transactions.Add(new CategoricalData { Category = "Jul", Value = 43 });
                        Transactions.Add(new CategoricalData { Category = "Sep", Value = 87 });
                        Transactions.Add(new CategoricalData { Category = "Oct", Value = 89 });
                        Transactions.Add(new CategoricalData { Category = "Nov", Value = 87 });
                        Transactions.Add(new CategoricalData { Category = "Dec", Value = 54 });
                        TotalAmount = "₦127,500.00";
                        break;
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteOverview()
        {
            try
            {
                IsOverviewTab = true;
                OverviewTabStyle = activeTab;
                HistoryTabStyle = inactiveTab;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteHistory()
        {
            try
            {
                IsOverviewTab = false;
                OverviewTabStyle = inactiveTab;
                HistoryTabStyle = activeTab;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        public void SetupHistoryItems(int position)
        {
            var list = new List<DashboardItemViewModel>();
            switch (position)
            {
                case 0:
                    HistoryItems = new List<HistoryItem>
                    {
                        new HistoryItem { Date = DateTime.Now.AddDays(-20), Amount = "₦1,000.00", Description = "OTAL INTEREST", TransactionType = TransactionType.Credit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-24), Amount = "₦12,000.00", Description = "VANULA LTD", TransactionType = TransactionType.Credit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-12), Amount = "₦10,290.00", Description = "LOREM IPSUM", TransactionType = TransactionType.Debit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-21), Amount = "₦2,000.00", Description = "ATM WITHDRAWAL", TransactionType = TransactionType.Credit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-10), Amount = "₦500.00", Description = "OTAL INTEREST", TransactionType = TransactionType.Debit },
                    };
                    break;
                case 1:
                    HistoryItems = new List<HistoryItem>
                    {
                        new HistoryItem { Date = DateTime.Now.AddDays(-9), Amount = "₦1,000.00", Description = "ATM WITHDRAWAL", TransactionType = TransactionType.Credit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-17), Amount = "₦1,230.00", Description = "USUM VENTURES", TransactionType = TransactionType.Debit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-23), Amount = "₦3,442.00", Description = "VANULA LTD", TransactionType = TransactionType.Credit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-10), Amount = "₦500.00", Description = "OTAL INTEREST", TransactionType = TransactionType.Debit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-18), Amount = "₦870.00", Description = "LOREM IPSUM", TransactionType = TransactionType.Debit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-23), Amount = "₦1,000.00", Description = "ATM WITHDRAWAL", TransactionType = TransactionType.Credit },
                        new HistoryItem { Date = DateTime.Now.AddDays(-15), Amount = "₦3,000.00", Description = "ATM WITHDRAWAL", TransactionType = TransactionType.Credit },
                    };
                    break;
            }


            foreach(var item in HistoryItems)
            {
                list.Add(new DashboardItemViewModel(ErrorManager, this, item));
            }
            HistoryItemsSource = new ObservableCollection<DashboardItemViewModel>(list);
        }

        public class HistoryItem
        {
            public DateTime Date { get; set; }
            public string DateDisplay { get { return Date.ToShortDateString(); } }
            public string Amount { get; set; }
            public string Description { get; set; }
            public string TransactionType { get; set; }
        }
    }
}

