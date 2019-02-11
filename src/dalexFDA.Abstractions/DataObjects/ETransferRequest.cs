using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class ETransferRequest
    {
        public string BeneficiaryAccountName { get; set; }
        public string PaymentPurpose { get; set; }
        public double DepositAmount { get; set; }
        public double TransferFee { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RefNumber { get; set; }
        public double InvestmentAmount { get; set; }
        public int Duration { get; set; }
    }
}
