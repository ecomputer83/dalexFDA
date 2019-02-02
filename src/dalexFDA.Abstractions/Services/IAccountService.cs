using System;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IAccountService
    {
        Task<SignupResponse> Signup(SignupRequest data);
        Task<SignupResponse> SignupExistingUser(SignupRequest data);
        Task<bool> ConfirmAccount(string phoneNumber, string code);
        Task<User> GetUserByPhoneNumber(string phoneNumber);
    }
}
