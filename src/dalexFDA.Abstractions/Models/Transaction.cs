using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class Transaction
    {
        public string TransactionNumber { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
        public string ClientNo { get; set; }
        public string Description { get; set; }
        public string PaymentReference { get; set; }
        public string Chamber { get; set; }
        public int status { get; set; }
    }
}
