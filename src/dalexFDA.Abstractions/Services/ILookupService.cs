using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface ILookupService
    {
        Task<List<Lookup>> GetBanks();
        Task<List<Lookup>> GetDeliveryModes();
        Task<List<Lookup>> GetContactTypes();
        Task<string> GetTermsAndConditions();
        Task<string> GetPrivacyPolicy();
    }
}
