using System.Reflection;
using FreshMvvm;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dalexFDA.Abstractions;
using dalexFDA.Data.Mock;
using dalexFDA.Data.WebServices;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using Plugin.DeviceInfo.Abstractions;
using Plugin.DeviceInfo;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace dalexFDA
{
    public partial class App : Application, IAppService
    {
        public object CurrentNavigation { get; set; }
        IEnvironmentConfiguration Config;
        public System.Action RegisterPushNotificationService { get; set; }

        public App()
        {
            var configurationService = FreshIOC.Container.Resolve<IConfigurationService>();
            Config = configurationService.Current;

            ISetting Settings = FreshIOC.Container.Resolve<ISetting>();
            Settings.StoreAndGenerateAppID();

            RegisterServices();
            InitializeComponent();
            StartApp(Settings.isFirstTime);
        }

        protected override void OnStart()
        {
            AppCenter.Start($"ios=366e60b4-d330-42dc-ac8f-8dda5594f106;" +
                            $"android=b21a2db4-47b4-41d6-b78a-653936fc8efa;",
                            typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void StartApp(bool firstTime)
        {
            Page page;

            if (Config.Mock.DisplayUnitTests)
            {
                page = FreshPageModelResolver.ResolvePageModel<UnitTestsViewModel>();
            }
            else
            {
                if (firstTime)
                {
                    page = FreshPageModelResolver.ResolvePageModel<WelcomeViewModel>();
                }
                else
                {
                    page = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
                }
            }


            var container = new CustomFreshNavigationContainer(page)
            {
                BarTextColor = Color.White
            };

            CurrentNavigation = container.Navigation;

            MainPage = container;
        }

        #region Start Flows

        public void StartMainFlow()
        {
            MainPage = new CustomNav();
        }

        public void StartKYCUpdate()
        {
            var page = FreshPageModelResolver.ResolvePageModel<UpdateKYCAccountViewModel>();
            StartFlow(page);
        }
        public void StartDashboard()
        {
            var page = FreshPageModelResolver.ResolvePageModel<DashboardViewModel>();
            StartFlow(page);
        }

        public void StartElectronicFundTransfer()
        {
            var page = FreshPageModelResolver.ResolvePageModel<DepositPaymentViewModel>();
            StartFlow(page);
        }

        public void StartManualBankDeposit()
        {
            var page = FreshPageModelResolver.ResolvePageModel<ManualDepositViewModel>();
            StartFlow(page);
        }

        public void StartAccountStatements()
        {
            var page = FreshPageModelResolver.ResolvePageModel<AccountStatementsViewModel>();
            StartFlow(page);
            StartFlow(page);
        }

        public void StartContactChange()
        {
            var page = FreshPageModelResolver.ResolvePageModel<UpdateContactInfoViewModel>();
            StartFlow(page);
        }

        public void StartTransferHistory()
        {
            var page = FreshPageModelResolver.ResolvePageModel<TransactionHistoryViewModel>();
            StartFlow(page);
        }

        public void StartMyProfile()
        {
            var page = FreshPageModelResolver.ResolvePageModel<MyProfileViewModel>();
            StartFlow(page);
        }

        public void Logout()
        {
            StartApp(false);
        }

        private void StartFlow(Page page)
        {
            var container = new FreshMvvm.FreshNavigationContainer(page);
            container.BarTextColor = Color.White;

            var menu = FreshIOC.Container.Resolve<IFreshNavigationService>(CustomNav.Name) as MasterDetailPage;
            menu.Detail = container;
            menu.IsPresented = false;
        }

        #endregion

        private void RegisterServices()
        {
            FreshIOC.Container.Register<IAppService>(this);
            FreshIOC.Container.Register<IErrorManager, ErrorManager>();
            FreshIOC.Container.Register<Acr.UserDialogs.IUserDialogs>(Acr.UserDialogs.UserDialogs.Instance); 
            FreshIOC.Container.Register<IDeviceInfo>(CrossDeviceInfo.Current);
            FreshIOC.Container.Register<IMedia>(CrossMedia.Current);
            FreshIOC.Container.Register<IConnectivity>(CrossConnectivity.Current);

            if (Config.Mock.Enabled)
            {
                FreshIOC.Container.Register<ILookupService, LookupService>();
                FreshIOC.Container.Register<IAccountService, Data.Mock.AccountService>();
                FreshIOC.Container.Register<IInvestmentService, Data.Mock.InvestmentService>();
                FreshIOC.Container.Register<IAuthenticationService, Data.Mock.AuthenticationService>();
            }
            else
            {
                FreshIOC.Container.Register<ILookupService, LookupService>();
                FreshIOC.Container.Register<IAccountService, Data.WebServices.AccountService>();
                FreshIOC.Container.Register<IInvestmentService, Data.WebServices.InvestmentService>();
                FreshIOC.Container.Register<IAuthenticationService, Data.WebServices.AuthenticationService>();
            }

            var assembly = typeof(App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
        }

        
    }
}
