using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Common;
using Android.OS;
using dalexFDA.Abstractions;
using FreshMvvm;
using Plugin.CurrentActivity;
using Firebase.Iid;
using Firebase.Messaging;
using Firebase;

namespace dalexFDA.Droid
{
    [Activity(Label = "Dalex", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IEnvironmentConfiguration Config;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);
            base.OnCreate(bundle);

            ContainerConfig.Load();

            var configService = FreshIOC.Container.Resolve<IConfigurationService>();
            var config = configService.Load();
            Config = config;

            Bootstrap_Init(bundle);
            
            Xamarin.Forms.Forms.Init(this, bundle);
            var formsApp = new App();
            formsApp.RegisterPushNotificationService = () => RegisterDeviceWithPushNotificationService();
            LoadApplication(formsApp);
            FirebaseApp app = FirebaseApp.InitializeApp(Android.App.Application.Context);
            SendRegistrationToServer(FirebaseInstanceId.Instance.Token);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RegisterDeviceWithPushNotificationService()
        {
            if(IsPlayServicesAvailable())
            FirebaseMessaging.Instance.SubscribeToTopic("all");

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

        public bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    //GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    Finish();
                }

                return false;
            }

            return true;
        }

        void SendRegistrationToServer(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                // Add custom implementation, as needed.
                var configService = FreshIOC.Container.Resolve<IConfigurationService>();
                var settingService = FreshIOC.Container.Resolve<ISetting>();
                var config = configService.Load();
                Config = config;

                PushNotificationRequest request = new PushNotificationRequest();
                request.PushNotificationAppID = Config.Push.AppId;
                request.PushNotificationService = "FCM";
                request.PushNotificationID = token;
                settingService.PushNotificationID = request.PushNotificationID;
                settingService.PushNotificationAppID = request.PushNotificationAppID;
                settingService.PushNotificationService = request.PushNotificationService;
            }
        }

    }
}

