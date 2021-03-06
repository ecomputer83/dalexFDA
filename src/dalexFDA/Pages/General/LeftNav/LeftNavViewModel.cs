﻿using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using Plugin.Connectivity.Abstractions;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class LeftNavViewModel: BaseViewModel
    {        
        readonly IErrorManager ErrorManager;                
        readonly IAppService AppService;
        readonly Acr.UserDialogs.IUserDialogs Dialog;
        readonly ISetting Setting;
        readonly ISession SessionService;
        readonly IConnectivity Connectivity;

        public bool IsPhotoAvailable { get { return !string.IsNullOrEmpty(PhotoSource); } }
        public bool IsPhotoNotAvailable { get { return !IsPhotoAvailable; } }
        public string PhotoSource { get; set; }
        public bool IsUserAccountActive { get; set; }
        public bool IsMakeDepositOpen { get; set; }
        public string UserFullName { get; set; }
        public string ClientID { get; set; }
        public string UserAddress { get; set; }

        //commands
        public Command Dashboard { get; private set; }
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
        public Command RO { get; private set; }
        public Command Logout { get; private set; }

        public LeftNavViewModel(IErrorManager ErrorManager, IAppService AppService, Acr.UserDialogs.IUserDialogs Dialog, ISetting setting, IConnectivity connectivity,
                                ISession SessionService)
        {
            //setup default services
            this.ErrorManager = ErrorManager;
            this.Connectivity = connectivity;
            //setup other services
            this.AppService = AppService;
            this.Dialog = Dialog;
            this.Setting = setting;
            this.SessionService = SessionService;

            //setup commands
            Dashboard = new Command(async () => await ExecuteDashboard());
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
            RO = new Command(async () => await ExecuteRO());
            Logout = new Command(async () => await ExecuteLogout());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                //isConnected = Connectivity.IsConnected;
                if (!Connectivity.IsConnected)
                {
                    throw new Exception("No internet connection, Please connect to internet");
                }

                App app = Application.Current as App;
                if (app.RegisterPushNotificationService != null)
                {
                    app.RegisterPushNotificationService();
                }
                UserFullName = SessionService.CurrentUser.Name;
                UserAddress = SessionService.CurrentUser.Address;
                ClientID = "Client ID: " + SessionService.CurrentUser.ClientNo;
                IsUserAccountActive = SessionService.CurrentUser.Status == UserAccountStatus.Active;
                IsMakeDepositOpen = false;
                PhotoSource = SessionService.CurrentUser?.PhotoUrl;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteDashboard()
        {
            try
            {
                AppService.StartDashboard();
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
                AppService.StartTransferHistory();
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
                AppService.StartNotification();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteRO()
        {
            try
            {
                AppService.StartRO();
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
                AppService.StartContactChange();
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
                AppService.StartEnquiry();
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
                AppService.StartFeedback();
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
                AppService.StartMyProfile();
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

