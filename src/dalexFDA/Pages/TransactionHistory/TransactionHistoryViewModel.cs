using Acr.UserDialogs;
using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
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
                    IsDepositActive = true;
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

                TransactionHistory = await IInvestmentService.GetTransactionHistory();

                if (TransactionHistory != null)
                    Deposits = TransactionHistory.Deposits;

                foreach (var item in Deposits)
                {
                    list.Add(new TransactionHistoryItemViewModel(this, item, ActiveTab));
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
                ActiveTab = TransactionHistoryTab.Deposit;
                IsRolloverActive = IsRedemptionActive = IsConsolidationActive = false;
                IsDepositActive = true;
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
                IsDepositActive = IsRedemptionActive = IsConsolidationActive = false;
                IsRolloverActive = true;
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
                IsDepositActive = IsRolloverActive = IsConsolidationActive = false;
                IsRedemptionActive = true;
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
                IsDepositActive = IsRolloverActive = IsRedemptionActive = false;
                IsConsolidationActive = true;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}