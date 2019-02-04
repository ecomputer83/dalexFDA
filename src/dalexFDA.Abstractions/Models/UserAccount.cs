using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class UserAccount
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get { return $"{firstName} {lastName}";  } }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
