﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using dalexFDA.Abstractions;

namespace dalexFDA.Data.WebServices
{
    public class AccountService : IAccountService
    {
        private readonly IEnvironmentConfiguration Config;

        public AccountService(IConfigurationService configurationService)
        {
            Config = configurationService.Current;
        }

        public async Task<bool> ConfirmAccount(string phoneNumber, string code)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.ConfirmAccount(phoneNumber, code);

            return response;
        }

        public async Task<UserAccount> GetUser()
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetUser();

            return response;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetUserByPhoneNumber(phoneNumber);

            return response;
        }

        public async Task<SignupResponse> Signup(SignupRequest data)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.Signup(data);

            return response;
        }

        public async Task<SignupResponse> SignupExistingUser(SignupRequest data)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.SignupExistingUser(data);

            return response;
        }
    }
}
