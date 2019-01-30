using System;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using Refit;

namespace dalexFDA.Data.WebServices
{
    [Headers("Accept: application/json")]
    public interface IDalexApi
    {
        #region Signup

        [Post("/api/Account/Register")]
        Task<SignupResponse> Signup([Body(BodySerializationMethod.Json)]SignupRequest data);

        [Post("/api/Account/VerifySmsToken")]
        Task<bool> ConfirmAccount(string PhoneNumber, string token);

        [Get("/api/Account/GetAccount")]
        Task<User> GetUserByPhoneNumber(string PhoneNumber);

        #endregion
    }
}
