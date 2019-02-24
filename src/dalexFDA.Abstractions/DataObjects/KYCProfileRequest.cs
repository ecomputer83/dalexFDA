using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class KYCProfileRequest
    {
        public string Address { get; set; }
        public string PostalAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string CopyOfValidId { get; set; }
        public string ProofOfResUtilityBill { get; set; }
        public string Nationality { get; set; }
        public string HomeTown { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime ExpiryDateOfId { get; set; }
    }
}
