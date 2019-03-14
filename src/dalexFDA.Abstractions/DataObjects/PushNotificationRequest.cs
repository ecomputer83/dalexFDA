using System;
namespace dalexFDA.Abstractions
{
    public class PushNotificationRequest
    {
        public string PushNotificationService { get; set; }
        public string PushNotificationAppID { get; set; }
        public string PushNotificationID { get; set; }
    }
}
