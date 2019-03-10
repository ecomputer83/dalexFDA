using System;
using dalexFDA.Abstractions;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace dalexFDA.Core
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

        private const string UserTokenKey = "user_token";
        private static readonly string UserTokenDefault = null;

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

        public string UserToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserTokenKey, UserTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserTokenKey, value);
            }
        }

        public string User_firstName {
            get
            {
                return AppSettings.GetValueOrDefault("firstname", UserTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("firstname", value);
            }
        }
    
        public string User_lastName
        {
            get
            {
                return AppSettings.GetValueOrDefault("lastname", UserTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("lastname", value);
            }
        }
        public string User_fullName
        {
            get
            {
                return AppSettings.GetValueOrDefault("fullname", UserTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("fullname", value);
            }
        }
        public string User_email
        {
            get
            {
                return AppSettings.GetValueOrDefault("email", UserTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("email", value);
            }
        }
        public string User_phoneNmuber
        {
            get
            {
                return AppSettings.GetValueOrDefault("phonenumber", UserTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("phonenumber", value);
            }
        }

        public bool isFirstTime
        {
            get
            {
                return AppSettings.GetValueOrDefault("isNotFirstTime", true);
            }
            set
            {
                AppSettings.AddOrUpdateValue("isNotFirstTime", value);
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
