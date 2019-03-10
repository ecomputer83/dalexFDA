using Acr.UserDialogs;
using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public enum TransactionHistoryTab
    {
        Deposit = 0,
        Rollover = 1,
        Redemption = 2,
        Consolidation = 3
    }

    public class TransactionHistoryViewModel : BaseViewModel
    {
        internal readonly IErrorManager ErrorManager;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;

        public TransactionHistory TransactionHistory { get; set; }

        public ObservableCollection<TransactionHistoryItemViewModel> HistoryItems { get; set; }
        public List<Deposit> Deposits { get; set; }
        public List<Rollover> Rollovers { get; set; }
        public List<Redemption> Redemptions { get; set; }
        public List<Consolidation> Consolidations { get; set; }

        public TransactionHistoryTab ActiveTab { get; set; }

        public bool IsDepositActive { get; set; }
        public bool IsRolloverActive { get; set; }
        public bool IsRedemptionActive { get; set; }
        public bool IsConsolidationActive { get; set; }

        public string DepositSvgColor { get { return IsDepositActive ? "000000=FFFFFF" : "000000=e6e7e8"; } }
        public string RolloverSvgColor { get { return IsRolloverActive ? "000000=FFFFFF" : "000000=e6e7e8"; } }
        public string RedemptionSvgColor { get { return IsRedemptionActive ? "000000=FFFFFF" : "000000=e6e7e8"; } }
        public string ConsolidationSvgColor { get { return IsConsolidationActive ? "000000=FFFFFF" : "000000=e6e7e8"; } }

        public Color DepositLineColor { get { return IsDepositActive ? Color.FromHex("FFFFFF") : Color.FromHex("6D9B36"); } }
        public Color RolloverLineColor { get { return IsRolloverActive ? Color.FromHex("FFFFFF") : Color.FromHex("6D9B36"); } }
        public Color RedemptionLineColor { get { return IsRedemptionActive ? Color.FromHex("FFFFFF") : Color.FromHex("6D9B36"); } }
        public Color ConsolidationLineColor { get { return IsConsolidationActive ? Color.FromHex("FFFFFF") : Color.FromHex("6D9B36"); } }

        public Command ViewDeposit { get; set; }
        public Command ViewRollover { get; set; }
        public Command ViewRedemption { get; set; }
        public Command ViewConsolidation { get; set; }

        private const string deposit_title = "Deposits Items";
        private const string rollover_title = "Rollover Items";
        private const string redemption_title = "Redemption Items";
        private const string consolidation_title = "Consolidation Items";

        public TransactionHistoryViewModel(IErrorManager ErrorManager, IInvestmentService IInvestmentService, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;

            ViewDeposit = new Command(async () => await ExecuteViewDeposit());
            ViewRollover = new Command(async () => await ExecuteViewRollover());
            ViewRedemption = new Command(async () => await ExecuteViewRedemption());
            ViewConsolidation = new Command(async () => await ExecuteViewConsolidation());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading("Loading..."))
                {
                    ActiveTab = TransactionHistoryTab.Deposit;
                    SetActiveTab(ActiveTab);
                    TransactionHistory = await IInvestmentService.GetTransactionHistory();
                    HistoryItems = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        public async Task<ObservableCollection<TransactionHistoryItemViewModel>> SetupHistoryItems()
        {
            try
            {
                var list = new List<TransactionHistoryItemViewModel>();

                if (TransactionHistory == null)
                    return null;

                if (ActiveTab == TransactionHistoryTab.Deposit)
                {
                    Deposits = TransactionHistory?.Deposits;
                    if (Deposits != null)
                    {
                        foreach (var item in Deposits)
                            list.Add(new TransactionHistoryItemViewModel(this, item));
                    }
                }
                else if (ActiveTab == TransactionHistoryTab.Rollover)
                {
                    Rollovers = TransactionHistory?.Rollovers;

                    if (Rollovers != null)
                    {
                        foreach (var item in Rollovers)
                            list.Add(new TransactionHistoryItemViewModel(this, item));
                    }
                }
                else if (ActiveTab == TransactionHistoryTab.Redemption)
                {
                    Redemptions = TransactionHistory?.Redemptions;
                    if (Redemptions != null)
                    {
                        foreach (var item in Redemptions)
                            list.Add(new TransactionHistoryItemViewModel(this, item));
                    }
                }
                else if (ActiveTab == TransactionHistoryTab.Consolidation)
                {
                    Consolidations = TransactionHistory?.Consolidations;
                    if (Consolidations != null)
                    {
                        foreach (var item in Consolidations)
                            list.Add(new TransactionHistoryItemViewModel(this, item));
                    }
                }

                var data = new ObservableCollection<TransactionHistoryItemViewModel>(list);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
                return null;
            }
        }

        private async Task ExecuteViewDeposit()
        {
            try
            {
                using (Dialog.Loading("Loading..."))
                {
                    ActiveTab = TransactionHistoryTab.Deposit;
                    SetActiveTab(ActiveTab);
                    HistoryItems = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteViewRollover()
        {
            try
            {
                using (Dialog.Loading("Loading..."))
                {
                    ActiveTab = TransactionHistoryTab.Rollover;
                    SetActiveTab(ActiveTab);
                    HistoryItems = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteViewRedemption()
        {
            try
            {
                using (Dialog.Loading("Loading..."))
                {
                    ActiveTab = TransactionHistoryTab.Redemption;
                    SetActiveTab(ActiveTab);
                    HistoryItems = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteViewConsolidation()
        {
            try
            {
                using (Dialog.Loading("Loading..."))
                {
                    ActiveTab = TransactionHistoryTab.Consolidation;
                    SetActiveTab(ActiveTab);
                    HistoryItems = await SetupHistoryItems();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private void SetActiveTab(TransactionHistoryTab activeTab)
        {
            switch(activeTab)
            {
                case TransactionHistoryTab.Deposit:
                    IsRolloverActive = IsRedemptionActive = IsConsolidationActive = false;
                    IsDepositActive = true;
                    Title = deposit_title;
                    break;
                case TransactionHistoryTab.Rollover:
                    IsDepositActive = IsRedemptionActive = IsConsolidationActive = false;
                    IsRolloverActive = true;
                    Title = rollover_title;
                    break;
                case TransactionHistoryTab.Redemption:
                    IsDepositActive = IsRolloverActive = IsConsolidationActive = false;
                    IsRedemptionActive = true;
                    Title = redemption_title;
                    break;
                case TransactionHistoryTab.Consolidation:
                    IsDepositActive = IsRolloverActive = IsRedemptionActive = false;
                    IsConsolidationActive = true;
                    Title = consolidation_title;
                    break;
                default:
                    IsRolloverActive = IsRedemptionActive = IsConsolidationActive = false;
                    IsDepositActive = true;
                    Title = deposit_title;
                    break;
            }
        }
    }
}