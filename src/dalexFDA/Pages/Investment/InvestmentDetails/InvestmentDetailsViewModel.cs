﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;
using static dalexFDA.DashboardViewModel;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class InvestmentDetailsViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService InvestmentService;
        readonly IUserDialogs Dialog;

        public InvestmentItem Investment { get; set; }
        public Color StatusColor
        {
            get
            {
                return IsStatusActive ? (Color)Application.Current.Resources["PalmLeaf"]
                                            : (Color)Application.Current.Resources["Red"];
            }
        }            
        public Style RolloverButtonStyle
        {
            get
            {
                return IsStatusActive ? (Style)Application.Current.Resources["DisabledButton"] 
                                            : (Style)Application.Current.Resources["PrimaryButton"];
            }
        }
        private bool isStatusActive = false;
        public bool IsStatusActive { get { return isStatusActive; } set { isStatusActive = value; } }
        public string AccountName { get; set; }

        public Command ViewCertificate { get; set; }
        public Command RedeemInvestment { get; set; }
        public Command RolloverInvestment { get; set; }

        Nav Data;
        public class Nav
        {
            public InvestmentItem Investment { get; set; }
        }

        public InvestmentDetailsViewModel(IErrorManager ErrorManager, ISession SessionService, IUserDialogs Dialog,
            IInvestmentService investmentService)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.InvestmentService = investmentService;
            this.Dialog = Dialog;

            ViewCertificate = new Command(async () => await ExecuteViewCertificate());
            RedeemInvestment = new Command(async () => await ExecuteRedeemInvestment());
            RolloverInvestment = new Command(async () => await ExecuteRolloverInvestment());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;
                if (Data != null)
                {
                    using (Dialog.Loading("Please wait..."))
                    {
                        Investment = await InvestmentService.GetInvestment(Data.Investment?.Id);
                        if (Investment == null)
                        {
                            Investment = Data.Investment;
                            
                        }
                        this.IsStatusActive = Investment?.RemainDays != "0";
                        Investment.AccountName = (string.IsNullOrEmpty(Investment.AccountName)) ? "Self" : Investment.AccountName;
                    }
                }
                AccountName = SessionService?.CurrentUser?.Name;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteViewCertificate()
        {
            try
            {
                await CoreMethods.DisplayAlert("View Certificate", "Coming Soon", "Ok");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteRedeemInvestment()
        {
            try
            {
                var nav = new RedemptionRequestViewModel.Nav { Investment = Investment };
                await CoreMethods.PushPageModel<RedemptionRequestViewModel>(nav);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteRolloverInvestment()
        {
            try
            {
                if (IsStatusActive)
                    return;

                var nav = new RolloverRequestViewModel.Nav { Investment = Investment };
                await CoreMethods.PushPageModel<RolloverRequestViewModel>(nav);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
