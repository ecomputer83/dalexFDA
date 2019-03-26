using System;
using dalexFDA.Abstractions;
using Foundation;
using FreshMvvm;
using UIKit;
using UserNotifications;
using Firebase.CloudMessaging;
using Firebase.Analytics;
namespace dalexFDA.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate
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
            Firebase.Core.App.Configure();
            global::Xamarin.Forms.Forms.Init();

            Settings = FreshIOC.Container.Resolve<ISetting>();
            Settings.StoreAndGenerateAppID();

            Bootstrap_Post_Forms_Init();

            App fomsApp = new App();

            fomsApp.RegisterPushNotificationService = () => RegisterDeviceWithPushNotificationService();
            LoadApplication(fomsApp);

            Bootstrap_Post_Forms_LoadApp();
            // Register your app for remote notifications.
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

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

                Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) => {
                    var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
                    // if you want to send notification per user, use this token
                    System.Diagnostics.Debug.WriteLine(newToken);

                    connectFCM();
                });
                
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
           //     #if DEBUG
           //         Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceToken, Firebase.InstanceID.ApnsTokenType.Sandbox);
           //     #endif
           //     #if RELEASE
			        //Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceToken, Firebase.InstanceID.ApnsTokenType.Prod);
           //     #endif

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
                    request.PushNotificationService = "FCM";
                    request.PushNotificationAppID = Config.Push.AppId;
                    request.PushNotificationID = DeviceToken;

                    Settings.PushNotificationID = request.PushNotificationID;
                    Settings.PushNotificationAppID = request.PushNotificationAppID;
                    Settings.PushNotificationService = request.PushNotificationService;
                    //session.PushNotification = request;
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
        // iOS 9 <=, fire when recieve notification foreground
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Generate custom event
            NSString[] keys = { new NSString("Event_type") };
            NSObject[] values = { new NSString("Recieve_Notification") };
            var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);

            // Send custom event
            Firebase.Analytics.Analytics.LogEvent("CustomEvent", parameters);

            if (application.ApplicationState == UIApplicationState.Active)
            {
                ProcessNotification(userInfo, true);
            }
        }

        // iOS 10, fire when recieve notification foreground
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var title = notification.Request.Content.Title;
            var body = notification.Request.Content.Body;
            ProcessNotification(title, body, true);
        }

        private void connectFCM()
        {
            Messaging.SharedInstance.Connect((error) =>
            {
                if (error == null)
                {
                    Messaging.SharedInstance.Subscribe("/topics/all");
                }
                System.Diagnostics.Debug.WriteLine(error != null ? "error occured" : "connect success");
            });
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
                NSDictionary custom = null;

                string messageBody = String.Empty;
                string messageTitle = String.Empty;

                if (aps != null)
                {
                    if (aps.ContainsKey(new NSString("alert")))
                        custom = aps[new NSString("alert")] as NSDictionary;
                }

                if (custom != null)
                {
                    if (custom.ContainsKey(new NSString("body")))
                        messageBody = (custom[new NSString("body")] as NSString).ToString();

                    if (custom.ContainsKey(new NSString("title")))
                        messageTitle = (custom[new NSString("title")] as NSString).ToString();
                }


                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(messageBody))
                    {
                        UIAlertView avAlert = new UIAlertView(messageTitle, messageBody, null, "OK", null);
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
        void ProcessNotification(string title, string body, bool fromFinishedLaunching)
        {
            try
            {
                Console.WriteLine("ProcessNotification");

                string messageBody = body;
                string messageTitle = title;

                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(messageBody))
                    {
                        UIAlertView avAlert = new UIAlertView(messageTitle, messageBody, null, "OK", null);
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
