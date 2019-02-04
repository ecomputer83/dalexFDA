using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class LeftNavViewModel: BaseViewModel
    {
        //default services
        internal readonly IErrorManager ErrorManager;

        //other services
        internal readonly IAppService AppService;
        internal readonly Acr.UserDialogs.IUserDialogs Dialog;
        internal readonly ISetting Setting;

        public bool IsMakeDepositOpen { get; set; }
        public string UserFullName { get; set; }
        //commands
        public Command MakeDeposit { get; private set; }
        public Command ElectronicFundTransfer { get; private set; }
        public Command ManualBankDeposit { get; private set; }
        public Command AccountStatements { get; private set; }
        public Command TransactionHistory { get; private set; }
        public Command Notifications { get; private set; }
        public Command UpdateContactInfo { get; private set; }
        public Command Enquiries { get; private set; }
        public Command Feedback { get; private set; }
        public Command MyProfile { get; private set; }
        public Command Logout { get; private set; }

        public LeftNavViewModel(IErrorManager ErrorManager, IAppService AppService, Acr.UserDialogs.IUserDialogs Dialog, ISetting setting)
        {
            //setup default services
            this.ErrorManager = ErrorManager;

            //setup other services
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.Setting = setting;

            //setup commands
            MakeDeposit = new Command(async () => await ExecuteMakeDeposit());
            ElectronicFundTransfer = new Command(async () => await ExecuteElectronicFundTransfer());
            ManualBankDeposit = new Command(async () => await ExecuteManualBankDeposit());
            AccountStatements = new Command(async () => await ExecuteAccountStatements());
            TransactionHistory = new Command(async () => await ExecuteTransactionHistory());
            Notifications = new Command(async () => await ExecuteNotifications());
            UpdateContactInfo = new Command(async () => await ExecuteUpdateContactInfo());
            Enquiries = new Command(async () => await ExecuteEnquiries());
            Feedback = new Command(async () => await ExecuteFeedback());
            MyProfile = new Command(async () => await ExecuteMyProfile());
            Logout = new Command(async () => await ExecuteLogout());

            Setup();
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                UserFullName = this.Setting.User_fullName;
                IsMakeDepositOpen = false;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteMakeDeposit()
        {
            try
            {
                IsMakeDepositOpen = !IsMakeDepositOpen;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteElectronicFundTransfer()
        {
            try
            {
                AppService.StartElectronicFundTransfer();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteManualBankDeposit()
        {
            try
            {
                AppService.StartManualBankDeposit();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteAccountStatements()
        {
            try
            {
                AppService.StartAccountStatements();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteTransactionHistory()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Transfer History", "Coming Soon", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteNotifications()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Notifications", "Coming Soon", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteUpdateContactInfo()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Update Contact Info", "Coming Soon", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteEnquiries()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Enquires", "Coming Soon", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteFeedback()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Feedback and Complaints", "Coming Soon", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteMyProfile()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("My Profile", "Coming Soon", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteLogout()
        {
            try
            {
                var title = "LOG OUT?";
                var message = "Are you sure you want to log out?";
                var cancelText = "Cancel";
                var okText = "Yes";

                var okTextSelected = await Dialog.ConfirmAsync(message, title, okText, cancelText);

                if (okTextSelected)
                {
                    AppService.Logout();
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}

