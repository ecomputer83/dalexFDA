using System;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IAuthenticationService
    {
        Task<object> Authenticate(LoginRequest request);
    }
}
