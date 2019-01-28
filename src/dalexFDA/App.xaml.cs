using System;
using System.Reflection;
using FreshMvvm;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dalexFDA.Abstractions;
using dalexFDA.Data.Mock;
//using dalexFDA.Data.Mock;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace dalexFDA
{
    public partial class App : Application, IAppService
    {
        public object CurrentNavigation { get; set; }

        public App()
        {
            InitializeComponent();
            RegisterServices();
            StartApp();
        }

        protected override void OnStart()
        {
            AppCenter.Start($"ios=29fd6c0a-2f69-4f66-9749-c55e91d0062c;" +
                            $"android=051857ad-487a-438d-8dbc-962a0ff2fad0;",
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

        public void StartApp()
        {
            Page page;

            //if (EnvironmentHelper.Configuration.IsInScreenUnitTestingMode)
            //{
               // page = FreshPageModelResolver.ResolvePageModel<UnitTestsViewModel>();
            //}
            //else
            //{
            page = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
            //}


            var container = new CustomFreshNavigationContainer(page);
            container.BarTextColor = Color.White;

            CurrentNavigation = container.Navigation;

            MainPage = container;
        }

        #region Start Flows

        public void StartMainFlow()
        {
            MainPage = new CustomNav();
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
            var page = FreshPageModelResolver.ResolvePageModel<DashboardViewModel>();
            StartFlow(page);
        }

        public void StartTransferHistory()
        {
            var page = FreshPageModelResolver.ResolvePageModel<DashboardViewModel>();
            StartFlow(page);
        }

        public void Logout()
        {
            StartApp();
        }

        private void StartFlow(Page page)
        {
            var container = new FreshMvvm.FreshNavigationContainer(page);
            container.BarTextColor = Color.White;

            var menu = FreshIOC.Container.Resolve<IFreshNavigationService>(CustomNav.Name) as MasterDetailPage;
            menu.Detail = container;
            menu.IsPresented = false;

            //if (EnvironmentHelper.Configuration.IsInScreenUnitTestingMode)
            //{
            //    MainPage = container;
            //}
            //else
            //{
            //}
        }

        #endregion

        private void RegisterServices()
        {
            FreshIOC.Container.Register<IAppService>(this);
            FreshIOC.Container.Register<IErrorManager, ErrorManager>();
            FreshIOC.Container.Register<Acr.UserDialogs.IUserDialogs>(Acr.UserDialogs.UserDialogs.Instance);

            //if (EnvironmentConfiguration.Pharmacy.UsesMockData)
            //{
            //    //FreshIOC.Container.Register<IMobileDeviceService, MockMobileDeviceService>();

            FreshIOC.Container.Register<IDepositService, DepositService>();

            //}

            var assembly = typeof(App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;
        }
    }
}
