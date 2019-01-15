using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenith.Abstractions;

namespace Zenith.Data.Mock
{
    public class DepositService : IDepositService
    {
        public DepositService()
        {
        }

        public Task<List<Bank>> GetBanksAsync()
        {
            var bankList = new List<Bank>
            {
                new Bank{ BankName="Access Bank", BankCode=303 },
                new Bank{ BankName="Zenith",BankCode=058 },
                new Bank{ BankName="First Bank", BankCode=014 }
            };
            return Task.FromResult(bankList);
        }
    }
}
