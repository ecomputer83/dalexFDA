using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class ContactRequest
    {
        public int RequestType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
