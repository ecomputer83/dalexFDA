using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IAccountService
    {
        Task ResetPin(ResetPinRequest request);
        Task<SignupResponse> Signup(SignupRequest data);
        Task<SignupResponse> SignupExistingUser(SignupRequest data);
        Task<bool> ConfirmAccount(string phoneNumber, string code);
        Task<string> PostQuery(int Id, QueryRequest request);
        Task<List<Notification>> GetNotification();
        Task<User> GetUserByPhoneNumber(string phoneNumber);
        Task<User> GetKYCAccountByPhoneNumber(string phoneExtension, string phoneNumber);
        Task<User> GetUser();
        Task<KYCApplication> GetApplication();
        Task<MobileDevice> GetDevice();
        Task<bool> GenerateSMSToken(string phoneNumber);
        Task<ROResponse> GetRO(string Id);
        Task<List<User>> GetUsers();
        Task<string> AddDocument(DocumentRequest request);
        Task<bool> UpdateKYCAccount(KYCProfileRequest request);
        Task<bool> UpdateMobileDevice(MobileDevice device);
        Task<string> AddTransaction(Transaction transaction);
        Task<Transaction> GetTransaction(int TransactionId);
    }
}
