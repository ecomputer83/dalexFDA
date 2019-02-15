using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public partial class TransactionHistory
    {
        public List<Deposit> Deposits { get; set; }
        public List<Consolidation> Consolidations { get; set; }
        public List<Redemption> Redemptions { get; set; }
        public List<Rollover> Rollovers { get; set; }
    }

    public partial class Consolidation
    {
        public string TransactionNo { get; set; }
        public string TransactionType { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestEarned { get; set; }
        public double ConsolidatedAmount { get; set; }
        public double NewInterestRate { get; set; }
    }

    public partial class Deposit
    {
        public string No { get; set; }
        public string PaymentType { get; set; }
        public DateTimeOffset DocumentDate { get; set; }
        public DateTimeOffset PostingDate { get; set; }
        public string DocumentType { get; set; }
        public double Amount { get; set; }
        public string BankAccountNo { get; set; }
        public DateTimeOffset PaymentConfirmedDate { get; set; }
    }

    public partial class Redemption
    {
        public string TransactionNo { get; set; }
        public string TransactionType { get; set; }
        public string CertificateNo { get; set; }
        public int Duration { get; set; }
        public double RetireAmount { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset RetireDate { get; set; }
    }

    public partial class Rollover
    {
        public string TransactionNo { get; set; }
        public string TransactionType { get; set; }
        public string CertificateNo { get; set; }
        public string InvestmentNo { get; set; }
        public int NewDuration { get; set; }
        public double RolloverAmount { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
