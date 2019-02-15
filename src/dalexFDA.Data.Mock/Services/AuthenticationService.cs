using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Data.Mock
{
    public class AuthenticationService : IAuthenticationService
    {
        IAccountService AccountService;

        public AuthenticationService(IAccountService accountService)
        {
            AccountService = accountService;
        }

        public async Task<AuthorizedAccount> Authenticate(LoginRequest request)
        {
            var retVal = new AuthorizedAccount();
            var users = await AccountService.GetUsers();
            var user = users.Find(x => x.PhoneNumber == request.username);
            
            if (user != null && user?.Password == request.password)
                retVal.access_token = request.username;
            else
                return null;

            return await Task.FromResult(retVal);
        }
    }
}
