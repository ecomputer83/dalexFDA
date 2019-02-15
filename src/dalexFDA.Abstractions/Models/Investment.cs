using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class RedeemInvestmentRequest
    {
        public string InvestmentId { get; set; }
        public double RedemptionAmount { get; set; }
        public double ReinvestmentAmount { get; set; }
        public int Duration { get; set; }
        public string SecurityAnswer { get; set; }
    }

    public class RolloverInvestmentRequest
    {
        public string InvestmentId { get; set; }
        public double ReinvestmentAmount { get; set; }
        public int Duration { get; set; }
        public string SecurityAnswer { get; set; }
    }

    public class InvestmentManualDeposit
    {
        public DateTime DepositDate { get; set; }
        public double DepositAmount { get; set; }
        public string BankName { get; set; }
        public string chequeNumber { get; set; }
        public double InvestmentAmount { get; set; }
        public int Duration { get; set; }
        public string SecurityAnswer { get; set; }
    }

    public class InvestmentEDeposit
    {
        public DateTime DepositDate { get; set; }
        public double DepositAmount { get; set; }
        public string PaymentReference { get; set; }
        public double InvestmentAmount { get; set; }
        public int Duration { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
