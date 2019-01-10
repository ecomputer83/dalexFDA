using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zenith.Abstractions
{
    public interface IDepositService
    {
        Task<List<Bank>> GetBanksAsync();
    }
}
