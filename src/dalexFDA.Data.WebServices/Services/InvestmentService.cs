using dalexFDA.Abstractions;
using dalexFDA.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Data.WebServices.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IEnvironmentConfiguration Config;

        public InvestmentService(IConfigurationService configurationService)
        {
            Config = configurationService.Current;
        }

        public async Task<bool> DepositEInvestment(InvestmentEDeposit request)
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

        public Task<bool> RedeemInvestment(RedeemInvestmentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RolloverInvestment(RolloverInvestmentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
