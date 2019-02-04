using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using PropertyChanged;

namespace dalexFDA
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

    [AddINotifyPropertyChangedInterface]
    public class DashboardViewModel : BaseViewModel
    {

        //default services
        internal readonly dalexFDA.Abstractions.IErrorManager ErrorManager;

        //other services
        internal readonly Acr.UserDialogs.IUserDialogs Dialog;

        //commands
        public Command Overview { get; private set; }
        public Command History { get; private set; }

        //properties
        public bool IsOverviewTab { get; set; }
        public string LastSession { get; set; }
        public string TotalAmount { get; set; }


        public ObservableCollection<DashboardItemViewModel> HistoryItemsSource { get; set; }
        public InvestmentAccount Account { get; set; }
        public List<InvestmentItem> Investments { get; set; }
        readonly IInvestmentService InvestmentService;

        public DashboardViewModel(dalexFDA.Abstractions.IErrorManager ErrorManager
          , Acr.UserDialogs.IUserDialogs Dialog, IInvestmentService investmentService)
        {
            //setup default services
            this.ErrorManager = ErrorManager;

            //setup other services
            this.Dialog = Dialog;

            InvestmentService = investmentService;

            //setup commands
            Overview = new Command(async () => await ExecuteOverview());
            History = new Command(async () => await ExecuteHistory());

        }
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

        public async void SetupHistoryItems()
        {
            try
            {

                var list = new List<DashboardItemViewModel>();
                Account = await InvestmentService.GetInvestmentAccount();
                if (Account != null)
                {
                    Investments = Account.Investments;
                }
                //Investments = new List<InvestmentItem>
                //{
                //    new InvestmentItem { Id = "INV00019", StartDate = DateTime.Now.AddDays(-20), Principal = "GHS 1,000,000.00", Days = "380", Rate = "23% p.a", Maturity="GHS 1,000,000.00", CertificateNumber = "DFC123456", Status = "Active" },
                //    new InvestmentItem { Id = "INV00020", StartDate = DateTime.Now.AddDays(-50), Principal = "GHS 10,000,000.00", Days = "380", Rate = "3.75% p.a", Maturity="GHS 10,000,000.00", CertificateNumber = "DFC123456", Status = "Active" },
                //    new InvestmentItem { Id = "INV00021", StartDate = DateTime.Now.AddDays(-70), Principal = "GHS 1,500,000.00", Days = "380", Rate = "3.75% p.a", Maturity="GHS 1,500,000.00", CertificateNumber = "DFC123456", Status = "Active" },
                //    new InvestmentItem { Id = "INV00022", StartDate = DateTime.Now.AddDays(-90), Principal = "GHS 1,700,000.00", Days = "380", Rate = "3.75% p.a", Maturity="GHS 1,700,000.00", CertificateNumber = "DFC123456", Status = "Active" },
                //    new InvestmentItem { Id = "INV00023", StartDate = DateTime.Now.AddDays(-120), Principal = "GHS 15,000,000.00", Days = "0", Rate = "12.75% p.a", Maturity="GHS 15,000,000.00", CertificateNumber = "DFC123456", Status = "Inactive" }
                //};

                foreach (var item in Investments)
                {
                    list.Add(new DashboardItemViewModel(ErrorManager, this, item));
                }
                HistoryItemsSource = new ObservableCollection<DashboardItemViewModel>(list);

            }
            catch (Exception ex)
            {

            }
        }

        private async Task ExecuteOverview()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Overview", "Overview", "OK");
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
                await this.CoreMethods.DisplayAlert("History", "History", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

    }
}

