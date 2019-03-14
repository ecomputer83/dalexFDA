using System;
namespace dalexFDA.Abstractions
{
    public interface ISession
    {
        User CurrentUser { get; set; }
        string Token { get; set; }
        PushNotificationRequest PushNotification { get; set; }
    }
}
