using System;
using dalexFDA.Abstractions;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace dalexFDA
{
    public class Settings : ISetting
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private static IDeviceInfo DeviceInfo
        {
            get
            {
                return CrossDeviceInfo.Current;
            }
        }

        #region Setting Constants

        private const string AppIDKey = "app_id";
        private static readonly string AppIDDefault = null;

        private const string PushNotificationIDKey = "push_notification_id";
        private static readonly string PushNotificationIDDefault = null;

        #endregion

        public string AppID
        {
            get
            {
                return AppSettings.GetValueOrDefault(AppIDKey, AppIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AppIDKey, value);
            }
        }

        public void StoreAndGenerateAppID()
        {
            if (string.IsNullOrEmpty(AppID))
            {
                //we don't have one - lets generate new app id and store back in settings
                AppID = DeviceInfo.GenerateAppId();
            }
        }
    }
}
