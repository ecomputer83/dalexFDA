using System;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;

namespace Zenith
{
    public class CustomNav : MasterDetailPage, IFreshNavigationService
    {
        public string NavigationServiceName { get; private set; }
        public const string MenuIcon = "Menu.png"; //"res:images.icn_hamburger";
        public const string Name = "CustomNav";

        public CustomNav()
        {
            NavigationServiceName = Name;
            SetMasterAndDetailPages();
            RegisterNavigation();
        }

        protected void SetMasterAndDetailPages()
        {
            var leftNavPage = FreshPageModelResolver.ResolvePageModel<LeftNavViewModel>() as LeftNavPage;
            leftNavPage.Title = "Menu";
            NavigationPage.SetHasNavigationBar(leftNavPage, false);

            leftNavPage.Icon = MenuIcon;

            this.Master = leftNavPage;

            var detailPage = FreshPageModelResolver.ResolvePageModel<DashboardViewModel>();

            var container = new FreshNavigationContainer(detailPage);
            container.BarTextColor = Color.White;
            //container.BarBackgroundColor = (Color)Application.Current.Resources["Green"];

            this.Detail = container;

            //show navs by default
            this.IsPresented = true;
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public void NotifyChildrenPageWasPopped()
        {
            if (Master is NavigationPage)
                ((NavigationPage)Master).NotifyAllChildrenPopped();

            if (((NavigationPage)this.Detail) is NavigationPage)
                (((NavigationPage)this.Detail)).NotifyAllChildrenPopped();
        }

        public virtual async Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                await Navigation.PopAsync();
            else
                await ((NavigationPage)this.Detail).PopAsync();
        }

        public virtual async Task PopToRoot(bool animate = true)
        {
            await ((NavigationPage)this.Detail).PopToRootAsync(animate);
        }

        public virtual async Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                await Navigation.PushModalAsync(new NavigationPage(page), animate);
            else
                await ((NavigationPage)this.Detail).PushAsync(page, animate);
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            throw new Exception("Cannot do this");
        }
    }
}
