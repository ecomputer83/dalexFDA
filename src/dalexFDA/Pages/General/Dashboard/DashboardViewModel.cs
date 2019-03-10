using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using PropertyChanged;
using System.Linq;

namespace dalexFDA.Core
{
    [AddINotifyPropertyChangedInterface]
    public class DashboardViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly Acr.UserDialogs.IUserDialogs Dialog;
        readonly IInvestmentService InvestmentService;
        readonly ISession SessionService;

        public bool IsOverviewTab { get; set; }
        public string LastSession { get; set; }

        public bool IsUserAccountActive { get; set; }
        public bool IsUserAccountInActive { get { return !IsUserAccountActive; } }

        public ObservableCollection<DashboardItemViewModel> InvestmentItemsSource { get; set; }
        public InvestmentAccount Account { get; set; }
        public List<InvestmentItem> Investments { get; set; }

        public Command UpdateKYCAccount { get; set; }
        public Command SearchCommand { get; set; }
        public Command CancelSearchCommand { get; set; }

        public DashboardViewModel(dalexFDA.Abstractions.IErrorManager ErrorManager, Acr.UserDialogs.IUserDialogs Dialog,
                                    IInvestmentService InvestmentService, ISession SessionService)
        {
            this.ErrorManager = ErrorManager;
            this.Dialog = Dialog;
            this.InvestmentService = InvestmentService;
            this.SessionService = SessionService;

            UpdateKYCAccount = new Command(async () => await ExecuteUpdateKYCAccount());
            SearchCommand = new Command<string>(async (data) => await ExecuteSearchCommand(data));
            CancelSearchCommand = new Command(async () => await ExecuteCancelSearchCommand());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading("Please wait..."))
                {
                    LastSession = DateTime.Now.ToUniversalTime().ToString();
                    IsOverviewTab = true;
                    InvestmentItemsSource = await SetupHistoryItems();
                    IsUserAccountActive = SessionService.CurrentUser?.Status == UserAccountStatus.Active;
                    
                    if (!IsUserAccountActive)
                    {
                        if (Account == null)
                            Account = new InvestmentAccount();

                        Account.TotalBalance = 0;
                        Account.ActiveInvestments = 0;
                        Account.InvestmentNearMaturity = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        public async Task<ObservableCollection<DashboardItemViewModel>> SetupHistoryItems()
        {
            try
            {
                var list = new List<DashboardItemViewModel>();
                Account = await InvestmentService.GetInvestmentAccount();
                if (Account != null) Investments = Account.Investments;


                if (Investments != null) foreach (var item in Investments) list.Add(new DashboardItemViewModel(ErrorManager, this, item));

                var data = new ObservableCollection<DashboardItemViewModel>(list);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
                return null;
            }
        }

        public async Task<ObservableCollection<DashboardItemViewModel>> SetupItems(IEnumerable<InvestmentItem> Investments)
        {
            try
            {
                var list = new List<DashboardItemViewModel>();
                if (Investments != null) foreach (var item in Investments) list.Add(new DashboardItemViewModel(ErrorManager, this, item));

                var data = new ObservableCollection<DashboardItemViewModel>(list);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
                return null;
            }
        }

        private async Task ExecuteUpdateKYCAccount()
        {
            try
            {
                await CoreMethods.PushPageModel<UpdateKYCAccountViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        protected async Task ExecuteSearchCommand(string NewTextValue)
        {

            try
            {
                using (Dialog.Loading("Please wait..."))
                {
                    if (String.IsNullOrEmpty(NewTextValue))
                    {
                        InvestmentItemsSource = await SetupHistoryItems();
                    }
                    else
                    {
                        var result = Investments.Where(c =>
                           ((!String.IsNullOrEmpty(c.AccountName)) &&
                                   c.AccountName.ToLower().Contains(NewTextValue.ToLower())
                                   )
                        );

                        this.InvestmentItemsSource = await SetupItems(result);
                    }
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }

        }

        protected async Task ExecuteCancelSearchCommand()
        {

            try
            {
                using (Dialog.Loading("Please wait..."))
                {
                    InvestmentItemsSource = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }

        }
    }
}

