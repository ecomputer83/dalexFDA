﻿using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Data.WebServices
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IEnvironmentConfiguration Config;

        public InvestmentService(IConfigurationService configurationService)
        {
            Config = configurationService.Current;
        }

        public async Task<bool> DepositEInvestment(ETransferRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.EDeposit(request);

            return response;
        }

        public async Task<bool> DepositManualInvestment(InvestmentManualDeposit request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.ManualDeposit(request);

            return response;
        }

        public async Task<InvestmentAccount> GetInvestmentAccount()
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetAccount();

            return response;
        }

        public async Task<InvestmentItem> GetInvestment(string Id)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetInvestment(Id);

            return response;
        }

        public async Task<TransactionHistory> GetTransactionHistory()
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetHistory();

            return response;
        }

        public async Task<bool> RedeemInvestment(RedeemInvestmentRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.Redeem(request);

            return response;
        }

        public async Task<bool> RolloverInvestment(RolloverInvestmentRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.Rollover(request);

            return response;
        }

        public async Task<bool> UpdateContact(ContactChangeRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.ContactEntry(request);

            return response;
        }

        public async Task<bool> RequestAccountStatement(StatementRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.StatementEntry(request);

            return response;
        }
    }
}
