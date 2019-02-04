using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class InvestmentAccount
    {
        public string UserId { get; set; }
        public string ClientId { get; set; }
        public string TotalBalance { get; set; }
        public int ActiveInvestments { get; set; }
        public int InvestmentNearMaturity { get; set; }

        public List<InvestmentItem> Investments { get; set; }
    }
}
