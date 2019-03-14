using System;
using dalexFDA.Abstractions;

namespace dalexFDA
{
    public class SessionService : ISession
    {
        public SessionService()
        {
        }

        public string Token { get; set; }
        public User CurrentUser { get; set; }
        public PushNotificationRequest PushNotification { get; set; }
    }
}
