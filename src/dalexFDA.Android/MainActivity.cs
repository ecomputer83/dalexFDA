﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using dalexFDA.Abstractions;
using FreshMvvm;
using Plugin.CurrentActivity;

namespace dalexFDA.Droid
{
    [Activity(Label = "Dalex", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IEnvironmentConfiguration Config;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            ContainerConfig.Load();

            var configService = FreshIOC.Container.Resolve<IConfigurationService>();
            var config = configService.Load();
            Config = config;

            Bootstrap_Init(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new dalexFDA.Core.App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void Bootstrap_Init(Bundle bundle)
        {
            //needs to be called before registering services on android because of acr dialog
            InitComponents(bundle);
            RegisterServices();
        }

        private void InitComponents(Bundle bundle)
        {
            Acr.UserDialogs.UserDialogs.Init(this);
            XamSvg.XamForms.Droid.SvgImageRenderer.InitializeForms();
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
        }

        public void RegisterServices()
        {
            //FreshIOC.Container.Register<IVersion, VersionService>();
        }

        public async override void OnBackPressed()
        {
            Acr.UserDialogs.IUserDialogs dialog = FreshIOC.Container.Resolve<Acr.UserDialogs.IUserDialogs>();
            IAppService app = FreshIOC.Container.Resolve<IAppService>();
            ISession session = FreshIOC.Container.Resolve<ISession>();

            if (session?.CurrentUser != null)
            {
                if(await dialog.ConfirmAsync("Are you sure you want to log out?"))
                {
                    base.OnBackPressed();
                }
                else
                {
                    app.StartMainFlow();
                }
                
            }
            else
            {
                base.OnBackPressed();
            }

            
        }
    }
}

