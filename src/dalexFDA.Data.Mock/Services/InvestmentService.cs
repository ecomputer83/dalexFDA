using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Data.Mock
{
    public class InvestmentService : IInvestmentService
    {
        ISession SessionService;
        List<InvestmentAccount> investmentAccounts = new List<InvestmentAccount>();

        public InvestmentService(ISession sessionService)
        {
            SessionService = sessionService;
            SetupInvestmentAccounts();
        }

        public async Task<bool> DepositEInvestment(ETransferRequest request)
        {
            await Task.Delay(25000);
            return await Task.FromResult(true);
        }

        public async Task<bool> DepositManualInvestment(InvestmentManualDeposit request)
        {
            await Task.Delay(2000);
            return await Task.FromResult(true);
        }

        public async Task<InvestmentAccount> GetInvestmentAccount()
        {
            await Task.Delay(1000);
            var account = investmentAccounts[0];
            return await Task.FromResult(account);
        }

        public async Task<bool> RedeemInvestment(RedeemInvestmentRequest request)
        {
            await Task.Delay(3500);
            return await Task.FromResult(true);
        }

        public async Task<bool> RolloverInvestment(RolloverInvestmentRequest request)
        {
            await Task.Delay(3000);
            return await Task.FromResult(true);
        }

        void SetupInvestmentAccounts()
        {
            investmentAccounts.AddRange(new List<InvestmentAccount>
            {
                new InvestmentAccount { ActiveInvestments = 4, ClientId = "H12", UserId = "G12", InvestmentNearMaturity = 1, TotalBalance = 1909200, Investments = new List<InvestmentItem>
                {
                    new InvestmentItem { Id = "INV000091", AccountName = "", CertificateNumber = "DFC123456", Status = "Active", Principal = 123000, Rate = "23% p.a", StartDate = new DateTime(2019,08,21), Maturity = 12000, MaturityDate = new DateTime(2020,2,12), InterestAmount = 2300, InterestEarned = 34300, Redemption = 102900, Days = "120" },
                    new InvestmentItem { Id = "INV000323", AccountName = "", CertificateNumber = "DFC425563", Status = "Active", Principal = 423000, Rate = "15% p.a", StartDate = new DateTime(2017,06,12), Maturity = 12000, MaturityDate = new DateTime(2017,12,19), InterestAmount = 6400, InterestEarned = 34300, Redemption = 62400, Days = "214" },
                    new InvestmentItem { Id = "INV000212", AccountName = "", CertificateNumber = "DFC523453", Status = "Active", Principal = 3266000, Rate = "23% p.a", StartDate = new DateTime(2017,05,23), Maturity = 12000, MaturityDate = new DateTime(2018,3,24), InterestAmount = 53300, InterestEarned = 43300, Redemption = 41900, Days = "22" },
                    new InvestmentItem { Id = "INV002312", AccountName = "", CertificateNumber = "DFC124121", Status = "Active", Principal = 8453000, Rate = "12% p.a", StartDate = new DateTime(2018,12,1), Maturity = 12000, MaturityDate = new DateTime(2019,6,10), InterestAmount = 12400, InterestEarned = 67300, Redemption = 16900, Days = "365" },
                    new InvestmentItem { Id = "INV000012", AccountName = "", CertificateNumber = "DFC010928", Status = "Active", Principal = 423000, Rate = "17.5% p.a", StartDate = new DateTime(2019,11,5), Maturity = 12000, MaturityDate = new DateTime(2020,3,11), InterestAmount = 3452, InterestEarned = 1232, Redemption = 1920, Days = "0" }
                } }
            });
        }
    }
}
