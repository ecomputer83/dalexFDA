using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class Notification
    {
        public DateTime NotificationDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool isSent { get; set; }
    }
}
