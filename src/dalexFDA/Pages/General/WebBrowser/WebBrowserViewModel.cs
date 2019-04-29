using dalexFDA.Abstractions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class WebBrowserViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAppService AppService;
        public string Url { get; set; }
        public int Id { get; set; }

        public Command Close { get; set; }
        public Command Done { get; set; }

        Nav Data;
        public class Nav
        {
            public int Id { get; set; }
            public string Url { get; set; }
        }

        public WebBrowserViewModel(IErrorManager ErrorManager, IAppService AppService)
        {
            this.ErrorManager = ErrorManager;
            this.AppService = AppService;
            Close = new Command(async () => await ExecuteClose());
            Done = new Command(async () => await ExecuteDone());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;
                if (Data != null)
                {
                    Url = Data.Url;
                    Id = Data.Id;
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteClose()
        {
            try
            {
                await CoreMethods.PopPageModel(true);
                //AppService.StartDashboard();
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
                await CoreMethods.PopPageModel(true);
                var nav = new DepositInvestmentDetailsViewModel.Nav { Id = this.Id };
                await CoreMethods.PushPageModel<DepositInvestmentDetailsViewModel>(nav);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
