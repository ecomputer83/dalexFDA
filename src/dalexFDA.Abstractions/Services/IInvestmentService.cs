using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IInvestmentService
    {
        Task<InvestmentAccount> GetInvestmentAccount();
        Task<InvestmentItem> GetInvestment(string Id);
        Task<bool> RedeemInvestment(RedeemInvestmentRequest request);
        Task<bool> RolloverInvestment(RolloverInvestmentRequest request);
        Task<bool> PostStatement(StatementRequest request);
        Task<bool> UpdateContact(ContactChangeRequest request);
        Task<bool> DepositManualInvestment(InvestmentManualDeposit request);
        Task<bool> DepositEInvestment(ETransferRequest request);
        Task<bool> RequestAccountStatement(AccountStatementRequest request);
        Task<bool> UpdateContactInfo(ContactRequest request);
        Task<TransactionHistory> GetTransactionHistory();
    }
}
