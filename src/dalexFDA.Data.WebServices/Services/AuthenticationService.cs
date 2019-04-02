using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using Refit;

namespace dalexFDA.Data.WebServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEnvironmentConfiguration Config;

        public AuthenticationService(IConfigurationService configurationService)
        {
            Config = configurationService.Current;
        }

        public async Task<AuthorizedAccount> Authenticate(LoginRequest request)
        {
                var service = RestServiceHelper.For<IDalexApi>(Config.Api);
                var response = await service.Login(request);

                return response;
        }
    }
}
