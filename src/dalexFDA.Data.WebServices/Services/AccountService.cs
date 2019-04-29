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

        public async Task<string> PostQuery(int Id, QueryRequest request)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.AddQuery(Id, request);

            return response;
        }

        public async Task<MobileDevice> GetDevice(){
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            return await service.GetDevice();
        }

        public async Task<bool> UpdateMobileDevice(MobileDevice device){
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            return await service.UpdateMobileDevice(device);
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
                    ExpiryDateOfId = response.ExpiryDateOfId,
                    Gender = response.Gender,
                    ContactName = response.ContactName,
                    ContactNumber = response.ContactNumber,
                    Occupation = response.Occupation,
                    Title = response.Title,
                    Group = response.Group,
                    Ro = response.ro
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

        public async Task<bool> GenerateSMSToken(string phoneNumber)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GenerateSMSToken(phoneNumber);

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

        public async Task<string> AddTransaction(Transaction transaction)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.AddTransaction(transaction);

            return response;
        }

        public async Task<Transaction> GetTransaction(int TransactionId)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetTransaction(TransactionId);

            return response;
        }

        public async Task<ROResponse> GetRO(string Id)
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetRo(Id);

            return response;
        }

        public async Task<List<Notification>> GetNotification()
        {
            var service = RestServiceHelper.For<IDalexApi>(Config.Api);
            var response = await service.GetNotfication();

            return response;
        }
    }
}
