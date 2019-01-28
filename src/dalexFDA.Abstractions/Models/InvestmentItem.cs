using System;
namespace dalexFDA.Abstractions
{
    public class InvestmentItem
    {
        public InvestmentItem()
        {
        }

        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public string Principal { get; set; }
        public string Days { get; set; }
        public string Rate { get; set; }
        public string Maturity { get; set; }
        public string CertificateNumber { get; set; }
        public string Status { get; set; }
        public string AccountName { get; set; }
        public DateTime MaturityDate { get; set; }
        public string InterestAmount { get; set; }
        public string InterestEarned { get; set; }
        public string Redemption { get; set; }
    }
}
