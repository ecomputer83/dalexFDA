﻿using System;
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

        [Post("/api/Account/RegisterasExist")]
        Task<SignupResponse> SignupExistingUser([Body(BodySerializationMethod.Json)]SignupRequest data);

        [Post("/api/Account/VerifySmsToken")]
        Task<bool> ConfirmAccount(string PhoneNumber, string token);

        [Get("/api/Account/GetAccount")]
        Task<User> GetUserByPhoneNumber(string PhoneNumber);

        [Post("/token")]
        [Headers("Content-Type: application/x-www-form-urlencoded; charset=UTF-8")]
        Task<AuthorizedAccount> Login([Body(BodySerializationMethod.UrlEncoded)]LoginRequest request);

        #endregion

        #region Authorization

        [Get("/api/Account/GetUser")]
        Task<UserAccount> GetUser();

        [Get("/api/Investment/GetAccount")]
        Task<InvestmentAccount> GetAccount();

        [Post("/api/Investment/Redeem")]
        Task<bool> Redeem([Body(BodySerializationMethod.Json)]RedeemInvestmentRequest request);

        [Post("/api/Investment/Rollover")]
        Task<bool> Rollover([Body(BodySerializationMethod.Json)]RolloverInvestmentRequest request);

        [Post("/api/Investment/ManualDeposit")]
        Task<bool> ManualDeposit([Body(BodySerializationMethod.Json)]InvestmentManualDeposit request);

        [Post("/api/Investment/EDeposit")]
        Task<bool> EDeposit([Body(BodySerializationMethod.Json)]ETransferRequest request);

        #endregion

    }
}
