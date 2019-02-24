using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class ResetPinRequest
    {
        public string PhoneNumber { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
