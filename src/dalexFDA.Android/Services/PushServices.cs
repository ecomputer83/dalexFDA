using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using PushSharp.Client;
using FreshMvvm;
using dalexFDA.Abstractions;
using dalexFDA;
using dalexFDA.Droid;

//VERY VERY VERY IMPORTANT NOTE!!!!
// Your package name MUST NOT start with an uppercase letter.
// Android does not allow permissions to start with an upper case letter
// If it does you will get a very cryptic error in logcat and it will not be obvious why you are crying!
// So please, for the love of all that is kind on this earth, use a LOWERCASE first letter in your Package Name!!!!

[assembly: Permission(Name = "com.codeware.dalexFDA.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.codeware.dalexFDA.permission.C2D_MESSAGE")]

[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace PushSharp.ClientSample.MonoForAndroid
{
    //You must subclass this!
    [BroadcastReceiver(Permission = GCMConstants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { GCMConstants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "com.codeware.dalexFDA" })]
    [IntentFilter(new string[] { GCMConstants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "com.codeware.dalexFDA" })]
    [IntentFilter(new string[] { GCMConstants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "com.codeware.dalexFDA" })]
    public class PushHandlerBroadcastReceiver : PushHandlerBroadcastReceiverBase<PushHandlerService>
    {
        static IEnvironmentConfiguration Config = FreshIOC.Container.Resolve<IConfigurationService>().Load();

        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project ID.
        //  Be sure to get the right Project ID from your Google APIs Console.  It's not the named project ID that appears in the Overview,
        //  but instead the numeric project id in the url: eg: https://code.google.com/apis/console/?pli=1#project:785671162406:overview
        //  where 785671162406 is the project id, which is the SENDER_ID to use!
        public static string[] SENDER_IDS = new string[] { Config.Push.ProjectId };

        public const string TAG = "PushSharp-GCM";
    }

    [Service] //Must use the service tag
    public class PushHandlerService : PushHandlerServiceBase
    {
        IEnvironmentConfiguration Config;

        public PushHandlerService() : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(PushHandlerBroadcastReceiver.TAG, "GCM Registered: " + registrationId);

            App app = App.Current as App;
            //Settings.PushNotificationID = registrationId;           uncomment later

            var configService = FreshIOC.Container.Resolve<IConfigurationService>();
            var sessionService = FreshIOC.Container.Resolve<ISession>();
            var config = configService.Load();
            Config = config;

            PushNotificationRequest request = new PushNotificationRequest();
            request.PushNotificationAppID = Config.Push.AppId;
            request.PushNotificationService = "GCM";
            request.PushNotificationID = registrationId;
            sessionService.PushNotification = request;


            //createNotification("PushSharp-GCM Registered...", "The device has been Registered, Tap to View!");
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(PushHandlerBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);
            //Remove from the web service
            //  var wc = new WebClient();
            //  var result = wc.UploadString("http://your.server.com/api/unregister/", "POST",
            //      "{ 'registrationId' : '" + lastRegistrationId + "' }");

            //createNotification("PushSharp-GCM Unregistered...", "The device has been unregistered, Tap to View!");
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(PushHandlerBroadcastReceiver.TAG, "GCM Message Received!");

            string from = "";
            string alert = "";


            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                {
                    switch (key.ToLower())
                    {
                        case "from":
                            from = intent.Extras.Get(key).ToString();
                            break;
                        case "alert":
                            alert = intent.Extras.Get(key).ToString();
                            break;
                    }
                }
            }


            createNotification("Dalex FDA", alert);
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(PushHandlerBroadcastReceiver.TAG, "Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(PushHandlerBroadcastReceiver.TAG, "GCM Error: " + errorId);
        }

        void createNotification(string title, string desc)
        {


            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetContentTitle(title)
                .SetContentText(desc)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}

