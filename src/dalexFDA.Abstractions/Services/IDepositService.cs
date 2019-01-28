using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IDepositService
    {
        Task<List<Bank>> GetBanksAsync();
    }
}
