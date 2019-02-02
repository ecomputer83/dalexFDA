using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;

namespace dalexFDA.Data.WebServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEnvironmentConfiguration Config;

        public AuthenticationService(IConfigurationService configurationService)
        {
            Config = configurationService.Current;
        }

        public async Task<object> Authenticate(LoginRequest request)
        {
            try
            {
                var service = RestServiceHelper.For<IDalexApi>(Config.Api);
                var response = await service.Login(request);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
