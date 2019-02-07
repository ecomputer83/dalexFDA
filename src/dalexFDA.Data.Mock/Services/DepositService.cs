using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dalexFDA.Abstractions;

namespace dalexFDA.Data.Mock
{
    public class DepositService : IDepositService
    {
        public DepositService()
        {
        }

        public Task<IEnumerable<Bank>> GetBanks()
        {
            var bankList = new List<Bank>
            {
                new Bank{ Name="Access Bank", Code=303 },
                new Bank{ Name="Zenith",Code=058 },
                new Bank{ Name="First Bank", Code=014 }
            };
            return Task.FromResult(bankList.OrderBy(x => x.Name).AsEnumerable());
        }
    }
}
