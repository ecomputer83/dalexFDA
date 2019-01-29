using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace dalexFDA.Droid
{
    [Activity(Label = "Dalex", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Bootstrap_Init();

            Rg.Plugins.Popup.Popup.Init(this, bundle);



            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        void Bootstrap_Init()
        {
            //needs to be called before registering services on android because of acr dialog
            InitComponents();

            RegisterServices();
        }

        private void InitComponents()
        {
            Acr.UserDialogs.UserDialogs.Init(this);
            XamSvg.XamForms.Droid.SvgImageRenderer.InitializeForms();
        }

        public void RegisterServices()
        {
            //FreshIOC.Container.Register<IVersion, VersionService>();
        }
    }
}

