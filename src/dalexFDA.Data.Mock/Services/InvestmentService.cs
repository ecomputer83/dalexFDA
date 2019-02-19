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
        TransactionHistory transactionHistory = new TransactionHistory();

        public InvestmentService(ISession sessionService)
        {
            SessionService = sessionService;
            SetupInvestmentAccounts();
            SetupTransactionHistory();
        }

        public async Task<bool> DepositEInvestment(ETransferRequest request)
        {
            await Task.Delay(2500);
            return await Task.FromResult(true);
        }

        public async Task<bool> DepositManualInvestment(InvestmentManualDeposit request)
        {
            await Task.Delay(2000);
            return await Task.FromResult(true);
        }

        public async Task<InvestmentItem> GetInvestment(string Id)
        {
            await Task.Delay(1000);
            var account = investmentAccounts[0];
            return await Task.FromResult(account.Investments[0]);
        }

        public async Task<InvestmentAccount> GetInvestmentAccount()
        {
            await Task.Delay(1000);
            var account = investmentAccounts[0];
            return await Task.FromResult(account);
        }

        public async Task<TransactionHistory> GetTransactionHistory()
        {
            return await Task.FromResult(transactionHistory);
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

        void SetupTransactionHistory()
        {
            var deposits = new List<Deposit>
            {
                new Deposit { No = "DFDDN00800", PaymentType = "Reinvest Deposit", DocumentDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), PostingDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), DocumentType = "Money Order", Amount = 20024.10959, BankAccountNo = "", PaymentConfirmedDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z") },
                new Deposit { No = "DFDDN00800", PaymentType = "Reinvest Deposit", DocumentDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), PostingDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), DocumentType = "Money Order", Amount = 120207.12329, BankAccountNo = "", PaymentConfirmedDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z") },
                new Deposit { No = "DFDDN00800", PaymentType = "Payment", DocumentDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), PostingDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), DocumentType = "Money Order", Amount = 120000, BankAccountNo = "ECB002", PaymentConfirmedDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z") },
                new Deposit { No = "DFDDN00800", PaymentType = "Payment", DocumentDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), PostingDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), DocumentType = "Deposit Slip", Amount = 150000, BankAccountNo = "ACB002", PaymentConfirmedDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z") },
                new Deposit { No = "DFDDN00800", PaymentType = "Payment", DocumentDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), PostingDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z"), DocumentType = "Deposit Slip", Amount = 10548.49, BankAccountNo = "GCB001", PaymentConfirmedDate = DateTimeOffset.Parse("2019-02-10T00:00:00Z") }
            };
            var rollovers = new List<Rollover>
            {
                new Rollover { TransactionNo = "DFDTN00006", TransactionType = "Rollover", CertificateNo = "DFDC00569", InvestmentNo = "DFDIN00644", NewDuration = 31, RolloverAmount = 120207.12329, ApprovalStatus = "Approved" },
                new Rollover { TransactionNo = "DFDTN09096", TransactionType = "Rollover", CertificateNo = "DFDC01323", InvestmentNo = "DFDIN12901", NewDuration = 12, RolloverAmount = 34132, ApprovalStatus = "Not Approved" }
            };
            var redemptions = new List<Redemption>
            {
                new Redemption { TransactionNo = "DFDTN00008", TransactionType = "Redemption", CertificateNo = "DFDC00571", Duration = 2, RetireAmount = 20000, StartDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z"), EndDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z"), RetireDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z") },
                new Redemption { TransactionNo = "DFDTN00007", TransactionType = "Redemption", CertificateNo = "DFDC00524", Duration = 15, RetireAmount = 62430, StartDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z"), EndDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z"), RetireDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z") },
                new Redemption { TransactionNo = "DFDTN00009", TransactionType = "Redemption", CertificateNo = "DFDC00643", Duration = 8, RetireAmount = 546400, StartDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z"), EndDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z"), RetireDate = DateTimeOffset.Parse("2019-02-08T00:00:00Z") }
            };
            var consolidations = new List<Consolidation>
            {
                new Consolidation { TransactionNo = "DFDTN00008", TransactionType = "Consolidation", PrincipalAmount = 30572.59959, InterestEarned = 235.89243, ConsolidatedAmount = 30808.49202, NewInterestRate = 23 }
            };

            transactionHistory = new TransactionHistory
            {
                Deposits = deposits,
                Rollovers = rollovers,
                Redemptions = redemptions,
                Consolidations = consolidations
            };
        }
    }
}
