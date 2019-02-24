using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using PropertyChanged;

namespace dalexFDA
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

        public DashboardViewModel(dalexFDA.Abstractions.IErrorManager ErrorManager, Acr.UserDialogs.IUserDialogs Dialog,
                                    IInvestmentService InvestmentService, ISession SessionService)
        {
            this.ErrorManager = ErrorManager;
            this.Dialog = Dialog;
            this.InvestmentService = InvestmentService;
            this.SessionService = SessionService;

            UpdateKYCAccount = new Command(async () => await ExecuteUpdateKYCAccount());
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
    }
}

