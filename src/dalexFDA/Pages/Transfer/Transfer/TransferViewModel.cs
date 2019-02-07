using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using dalexFDA.Abstractions;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;

namespace  dalexFDA
{
    public partial class TransferViewModel: BaseViewModel
    {
        readonly INavigation CurrentNavigation;
        public bool isForm { get; set; }
        public bool isSummary { get; set; }
        public bool isComplete { get; set; }
        public string FromAccountNumber { get; set; }
        public string AccountNumber { get; set; }
        public object TransferTypeItem { get; set; }
        public string TransferTypeName { get; set; }
        public bool isOtherBank { 
            get
            {
                if (TransferTypeItem == null) return false;
                var item = TransferTypeItem as TransferType;
                return (item.TransferId == 2) ? true : false;
            } 
        }
        public bool showAccount
        {
            get
            {
                return (AccountName != null) ? true : false;
            }
        }
        public ObservableCollection<Bank> Banks { get; set; }
        public int BankCode { get; set; }
        public TransferViewModel()
        {
            CurrentNavigation = AppService.CurrentNavigation as INavigation;
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                isForm = true;
                PopulateTransferType();
                PopulateFromAccount();
                PopulateBanks();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private void PopulateTransferType()
        {
            TransferType = new ObservableCollection<TransferType>
            {
                new Abstractions.TransferType { TransferId=1, TransferName = "To Zenith Account" },
                new Abstractions.TransferType { TransferId=2, TransferName = "To Other Bank" }
                //new Abstractions.TransferType { TransferId=3, TransferName = "Open New Account" }
            };
        }

        private void PopulateBanks()
        {
            Banks = new ObservableCollection<Bank>
            {
                new Bank { Code=020, Name="Heritage Bank"},
                new Bank { Code=011, Name="Guarantee Trust Bank"},
                new Bank { Code=100, Name="Diamond Bank"},
                new Bank { Code=065, Name="Sterling Bank"},
                new Bank { Code=098, Name="First Bank"},
                new Bank { Code=008, Name="United Bank"},
                new Bank { Code=007, Name="Union Bank of Africa"}
            };
        }

        private void PopulateFromAccount() 
        {
            FromAccount = new ObservableCollection<DashboardAccountItem>
            {
                new DashboardAccountItem { AccountNumber="1234515612", AccountType = "CURRENT ACCOUNT", AccountName = "JUDITH SALMON", Amount="₦75,000" },
                new DashboardAccountItem { AccountNumber="4714123411", AccountType = "SAVINGS ACCOUNT", AccountName = "SALMON JUDITH", Amount="₦127,500" }
            };
        }

        private void PopulateBeneficiary(int transferId = 1)
        {
            var fromBeneficiary = new ObservableCollection<Beneficiary>
            {
                new Beneficiary { AccountNumber="2175128888", AccountName = "BAKARE SULAIMON", BankCode=057, BankName="Zenith Bank" },
                new Beneficiary { AccountNumber="0018294472", AccountName = "ONISEMO BIOLA", BankCode=011, BankName="Guarantee Trust Bank" },
                new Beneficiary { AccountNumber="2114537545", AccountName = "OLATUNJI PELUMI", BankCode=100, BankName="Diamond Bank" },
                new Beneficiary { AccountNumber="1175954354", AccountName = "MBETINE MICHAEL", BankCode=011, BankName="Guarantee Trust Bank" },
                new Beneficiary { AccountNumber="2005762340", AccountName = "OYEDELE BOLANLE", BankCode=057, BankName="Zenith Bank" },
                new Beneficiary { AccountNumber="1011564327", AccountName = "NWABEKE CHIDINMA", BankCode=057, BankName="Diamond Bank" },
                new Beneficiary { AccountNumber="2001453864", AccountName = "GIWA AYOMIDE", BankCode=057, BankName="Zenith Bank" },
                new Beneficiary { AccountNumber="2135460348", AccountName = "ONI OLUWATOSIN", BankCode=057, BankName="Zenith Bank" },
                new Beneficiary { AccountNumber="1011564327", AccountName = "NWABEKE CHIDINMA", BankCode=057, BankName="Diamond Bank" }
            };
            if(transferId == 1){
                FromBeneficiary = fromBeneficiary.Where(cw => cw.BankCode == 057).ToList();
            }else if(transferId == 2){
                FromBeneficiary = fromBeneficiary.Where(cw => cw.BankCode != 057).ToList();
            }
        }

        private async Task ExecuteContinue()
        {
            try
            {
                Amount = $"₦{Amount}";
                isForm = false;
                isSummary = true;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteDone()
        {
            try
            {
                using (Dialog.Loading())
                {
                    await Task.Delay(3000);
                    isForm = false;
                    isSummary = false;
                    isComplete = true;
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteBack()
        {
            try
            {
                await this.CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteOpenAccount()
        {
            try
            {
                var type = TransferTypeItem as TransferType;
                TransferTypeName = type.TransferName;
                var response = await MyAccountPopupDialog.ShowDialog(CurrentNavigation, FromAccount);
                var accountItem = response.Item as DashboardAccountItem;
                FromAccountNumber = accountItem.AccountNumber;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteOpenBeneficiary()
        {
            try
            {
                var type = TransferTypeItem as TransferType;
                TransferTypeName = type.TransferName;
                PopulateBeneficiary(type.TransferId);
                var response = await BeneficiaryPopupDialog.ShowDialog(CurrentNavigation, FromBeneficiary);
                var beneficiaryItem = response.Item as Beneficiary;
                AccountNumber = beneficiaryItem.AccountNumber;
                AccountName = beneficiaryItem.AccountName;
                BankName = beneficiaryItem.BankName;
                BankCode = beneficiaryItem.BankCode;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteOpenBank()
        {
            try
            {
                var response = await BankPopupDialog.ShowDialog(CurrentNavigation, Banks);
                var accountItem = response.Item as Bank;
                BankName = accountItem.Name;
                BankCode = accountItem.Code;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteTextChanged()
        {
            try
            {
                var type = TransferTypeItem as TransferType;
                if(type.TransferId == 1){
                    if (AccountNumber.Length == 10)
                    {
                        AccountName = "Eminent Technology";
                        BankCode = 057;
                        BankName = "Zenith Bank";
                    }
                }else if(type.TransferId == 2){
                    if(BankCode != 0  && AccountNumber.Length == 10){
                        AccountName = "Otusheso Bola";
                    }
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}

