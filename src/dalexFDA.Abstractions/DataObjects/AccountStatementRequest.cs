using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class AccountStatementRequest
    {
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string DeliveryMode { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
