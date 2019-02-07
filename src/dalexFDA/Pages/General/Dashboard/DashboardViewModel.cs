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
        readonly IErrorManager ErrorManager;
        readonly Acr.UserDialogs.IUserDialogs Dialog;
        readonly IInvestmentService IInvestmentService;

        public bool IsOverviewTab { get; set; }
        public string LastSession { get; set; }
        public string TotalAmount { get; set; }

        public ObservableCollection<DashboardItemViewModel> InvestmentItemsSource { get; set; }
        public InvestmentAccount Account { get; set; }
        public List<InvestmentItem> Investments { get; set; }

        public DashboardViewModel(dalexFDA.Abstractions.IErrorManager ErrorManager, Acr.UserDialogs.IUserDialogs Dialog,
                                    IInvestmentService IInvestmentService)
        {
            this.ErrorManager = ErrorManager;
            this.Dialog = Dialog;
            this.IInvestmentService = IInvestmentService;
        }
        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading("Loading..."))
                {
                    LastSession = DateTime.Now.ToUniversalTime().ToString();
                    IsOverviewTab = true;
                    SetupTransactions();
                    InvestmentItemsSource = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        public void SetupTransactions()
        {
            TotalAmount = "GHC 999,999,000.00";
        }

        public async Task<ObservableCollection<DashboardItemViewModel>> SetupHistoryItems()
        {
            try
            {
                var list = new List<DashboardItemViewModel>();
                Account = await IInvestmentService.GetInvestmentAccount();
                if (Account != null)
                    Investments = Account.Investments;

                foreach (var item in Investments)
                {
                    list.Add(new DashboardItemViewModel(ErrorManager, this, item));
                }

                var data = new ObservableCollection<DashboardItemViewModel>(list);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
                return null;
            }
        }
    }
}

