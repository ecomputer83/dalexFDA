using System;

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
            fomsApp.RegisterPushNotificationService = () => RegisterDeviceWithPushNotificationService();
            LoadApplication(formsApp);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RegisterDeviceWithPushNotificationService()
        {
            ISetting Settings = FreshIOC.Container.Resolve<ISetting>();
            ISession session = FreshIOC.Container.Resolve<ISession>();
            try
            {
                bool registerWithServer = false;
                //Check to ensure everything's setup right
                PushClient.CheckDevice(this);
                PushClient.CheckManifest(this);


                //If it's empty, we need to register
                if (!PushClient.IsRegistered(this))
                {

                    PushClient.Register(this, PushSharp.ClientSample.MonoForAndroid.PushHandlerBroadcastReceiver.SENDER_IDS);
                    registerWithServer = true;
                }

                if (!registerWithServer)
                {
                    registerWithServer = !PushClient.IsRegisteredOnServer(this);
                }

                if (registerWithServer)
                {
                    Console.WriteLine("RegisterWithServer");
                    string registrationId = PushClient.GetRegistrationId(this);

                    Settings.PushNotificationID = registrationId;

                    App app = Xamarin.Forms.Application.Current as App;
                    PushNotificationRequest request = new PushNotificationRequest();
                    request.PushNotificationService = "GCM";
                    request.PushNotificationAppID = Config.Push.AppId;
                    request.PushNotificationID = registrationId;

                    session.PushNotification = request;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegisterDeviceWithPushNotificationService. Error - {ex.Message} - {ex}");
            }
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

