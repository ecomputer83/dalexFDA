using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions.Services
{
    public interface IInvestmentService
    {
        Task<InvestmentAccount> GetInvestmentAccount();
        Task<bool> RedeemInvestment(RedeemInvestmentRequest request);
        Task<bool> RolloverInvestment(RolloverInvestmentRequest request);
        Task<bool> DepositManualInvestment(InvestmentManualDeposit request);
        Task<bool> DepositEInvestment(InvestmentEDeposit request);
    }
}
