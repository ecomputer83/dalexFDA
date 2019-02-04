using System;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IAuthenticationService
    {
        Task<AuthorizedAccount> Authenticate(LoginRequest request);
    }
}
