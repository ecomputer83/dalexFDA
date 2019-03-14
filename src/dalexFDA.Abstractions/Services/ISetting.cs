using System;
namespace dalexFDA.Abstractions
{
    public interface ISetting
    {
        string AppID { get; set; }
        string UserToken { get; set; }
        string User_firstName { get; set; }
        string User_lastName { get; set; }
        string User_fullName { get; set; }
        string User_email { get; set; }
        string User_phoneNmuber { get; set; }
        bool isFirstTime { get; set; }
        string PushNotificationID { get; set; }
        void StoreAndGenerateAppID();
    }
}
