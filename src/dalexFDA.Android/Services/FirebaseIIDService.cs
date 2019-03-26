using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using dalexFDA.Abstractions;
using Firebase.Iid;
using FreshMvvm;

namespace dalexFDA.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseIIDService : FirebaseInstanceIdService
    {
        IEnvironmentConfiguration Config;
        const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServer(refreshedToken);
        }

        void SendRegistrationToServer(string token)
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