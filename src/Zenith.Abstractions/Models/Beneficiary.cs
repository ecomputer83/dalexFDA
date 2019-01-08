using System;
namespace Zenith.Abstractions
{
    public class Beneficiary
    {
        public Beneficiary()
        {
        }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public int BankCode { get; set; }
    }
}
