using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class UserAccount
    {
        public string clientNo { get; set; }
        public string phoneNumber { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string securityQuestion { get; set; }
        public string securityAnswer { get; set; }
        public string securityHint { get; set; }
        public string status { get; set; }
        public string PostalAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string CopyOfValidId { get; set; }
        public string ProofOfResUtilityBill { get; set; }
        public string Nationality { get; set; }
        public string HomeTown { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime ExpiryDateOfId { get; set; }
    }

    public class KYCApplication
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Phone_No { get; set; }
        public string Application_Status { get; set; }
    }
}
