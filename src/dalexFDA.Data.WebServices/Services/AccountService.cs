using System;
using System.Collections.Generic;
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

        public async Task<User> GetUser()
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetUser();

            if (response != null)
            {
                User user = new User
                {
                    Name = response.fullName,
                    Email = response.email,
                    PhoneNumber = response.phoneNumber,
                    SecurityQuestion = response.securityQuestion,
                    SecurityAnswer = response.securityAnswer,
                    SecurityHint = response.securityHint,
                    Address = response.address,
                    ClientNo = response.clientNo,
                    Status = response.status,
                    PostalAddress = response.PostalAddress,
                    BirthDate = response.BirthDate,
                    PlaceOfBirth = response.PlaceOfBirth,
                    PhotoUrl = response.PhotoUrl,
                    CopyOfValidId = response.CopyOfValidId,
                    ProofOfResUtilityBill = response.ProofOfResUtilityBill,
                    Nationality = response.Nationality,
                    HomeTown = response.HomeTown,
                    ExpiryDateOfId = response.ExpiryDateOfId
                };
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<KYCApplication> GetApplication()
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            return await service.GetApplication();

        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetUserByPhoneNumber(phoneNumber);

            return response;
        }

        public async Task<User> GetKYCAccountByPhoneNumber(string phoneExtension, string phoneNumber)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetKYCUserByPhoneNumber(phoneNumber, phoneExtension);

            return response;
        }

        public Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
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

        public async Task<string> AddDocument(DocumentRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.AddDocument(request);

            return response;
        }

        public async Task<bool> UpdateKYCAccount(KYCProfileRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.UpdateKYCAccount(request);

            return response;
        }

        public async Task ResetPin(ResetPinRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            await service.ResetPassword(request);

            //return response;
        }
    }
}
