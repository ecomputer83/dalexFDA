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
        public double Principal { get; set; }
        public string Days { get; set; }
        public string Rate { get; set; }
        public double Maturity { get; set; }
        public string CertificateNumber { get; set; }
        public string Status { get; set; }
        public string AccountName { get; set; }
        public DateTime MaturityDate { get; set; }
        public double InterestAmount { get; set; }
        public double InterestEarned { get; set; }
        public double Redemption { get; set; }
    }
}
