using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class ETransferRequest
    {
        public DateTime DepositDate { get; set; }
        public double DepositAmount { get; set; }
        public string PaymentReference { get; set; }
        public double InvestmentAmount { get; set; }
        public int Duration { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
