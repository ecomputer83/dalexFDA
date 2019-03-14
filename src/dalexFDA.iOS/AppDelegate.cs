using System;
using System.Collections.Generic;
using System.Linq;
using dalexFDA.Abstractions;
using Foundation;
using FreshMvvm;
using UIKit;

namespace dalexFDA.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        ISetting Settings;
        IEnvironmentConfiguration Config;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ContainerConfig.Load();

            var configService = FreshIOC.Container.Resolve<IConfigurationService>();
            var config = configService.Load();
            Config = config;

            Bootstrap_Init();

            global::Xamarin.Forms.Forms.Init();

            Settings = FreshIOC.Container.Resolve<ISetting>();
            Settings.StoreAndGenerateAppID();

            Bootstrap_Post_Forms_Init();

            App fomsApp = new App();

            fomsApp.RegisterPushNotificationService = () => RegisterDeviceWithPushNotificationService();
            LoadApplication(fomsApp);

            Bootstrap_Post_Forms_LoadApp();

            return base.FinishedLaunching(app, options);
        }

        void Bootstrap_Init()
        {
            Rg.Plugins.Popup.Popup.Init();
            RegisterServices();
        }

        void Bootstrap_Post_Forms_Init()
        {
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
        }

        void Bootstrap_Post_Forms_LoadApp()
        {
            Appearance.Configure();
            XamSvg.XamForms.iOS.SvgImageRenderer.InitializeForms();

#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }

        #region Push Notification Handling

        public void RegisterDeviceWithPushNotificationService()
        {
            try
            {
                UIUserNotificationType notificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(notificationTypes, new NSSet(new string[] { }));

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
            }
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            try
            {
                ISession session = FreshIOC.Container.Resolve<ISession>();
                Console.WriteLine("RegisteredForRemoteNotifications: {0}", deviceToken);

                string LastDeviceToken = Settings.PushNotificationID;

                string MobileDeviceID = Settings.AppID;
                //Get New Device Token
                var newDeviceToken = deviceToken.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");

                //We only want to send the device token to the server if it hasn't changed since last time
                string DeviceToken = newDeviceToken.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");
                if (!DeviceToken.Equals(LastDeviceToken))
                {
                    Settings.PushNotificationID = DeviceToken;

                    App app = Xamarin.Forms.Application.Current as App;
                    PushNotificationRequest request = new PushNotificationRequest();
                    request.PushNotificationService = "APNS";
                    request.PushNotificationAppID = Config.Push.AppId;
                    request.PushNotificationID = DeviceToken;


                    session.PushNotification = request;
                }
            }
            catch (Exception ex)
            {
                UIAlertView avAlert = new UIAlertView("Failed Push Remote Registration", ex.Message, null, "OK", null);
                avAlert.Show();
            }
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            Console.WriteLine("ReceivedRemoteNotification");
            ProcessNotification(userInfo, false);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Console.WriteLine("DidReceiveRemoteNotification");
            ProcessNotification(userInfo, false);
        }

        public override void DidFailToContinueUserActivitiy(UIApplication application, string userActivityType, NSError error)
        {
            UIAlertView avAlert = null;

            try
            {
                avAlert = new UIAlertView("Failed Push Notification Registration", error.Description, null, "OK", null);
                avAlert.Show();
            }
            catch (Exception ex)
            {
                avAlert = new UIAlertView("Failed Push Notification Registration", ex.Message, null, "OK", null);
                avAlert.Show();
            }
        }

        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            try
            {
                Console.WriteLine("ProcessNotification");

                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;
                NSDictionary custom = options.ObjectForKey(new NSString("custom")) as NSDictionary;

                string alert = String.Empty;
                string messageType = String.Empty;
                string messageIncidentID = String.Empty;

                if (aps != null)
                {
                    if (aps.ContainsKey(new NSString("alert")))
                        alert = (aps[new NSString("alert")] as NSString).ToString();
                }

                if (custom != null)
                {
                    if (custom.ContainsKey(new NSString("type")))
                        messageType = (custom[new NSString("type")] as NSString).ToString();

                    if (custom.ContainsKey(new NSString("id")))
                        messageIncidentID = (custom[new NSString("id")] as NSString).ToString();
                }


                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                        avAlert.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                UIAlertView alert = new UIAlertView("Failed Processing Push Notification", ex.Message, null, "OK", null);
                alert.Show();
            }
        }

        #endregion
        public void RegisterServices()
        {
            //FreshIOC.Container.Register<IAuthService, AuthService>();
        }
    }
}
