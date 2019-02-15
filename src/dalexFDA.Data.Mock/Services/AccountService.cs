using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Data.Mock
{
    public class AccountService : IAccountService
    {
        ISession SessionService;
        List<User> users = new List<User>();

        public AccountService(ISession sessionService)
        {
            SessionService = sessionService;
            SetupUsers();
        }

        public async Task<bool> ConfirmAccount(string phoneNumber, string code)
        {
            await Task.Delay(500);
            return await Task.FromResult(true);
        }

        public async Task<List<User>> GetUsers()
        {
            return await Task.FromResult(users);
        }

        public async Task<User> GetUser()
        {
            await Task.Delay(500);
            var phoneNumber = SessionService.Token;
            var user = users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            return await Task.FromResult(user);
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            await Task.Delay(200);
            var user = users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            return await Task.FromResult(user);
        }

        public async Task<User> GetKYCAccountByPhoneNumber(string phoneExtension, string phoneNumber)
        {
            await Task.Delay(700);
            var user = users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            return await Task.FromResult(user);
        }

        public async Task<SignupResponse> Signup(SignupRequest data)
        {
            await Task.Delay(1000);
            var retVal = new SignupResponse();
            return await Task.FromResult(retVal);
        }

        public async Task<SignupResponse> SignupExistingUser(SignupRequest data)
        {
            await Task.Delay(800);
            var retVal = new SignupResponse();
            return await Task.FromResult(retVal);
        }

        void SetupUsers()
        {
            users.AddRange(new List<User>
            {
                new User { Id = new Guid("32999016-4DA7-409D-8D5E-F72736DAD00D"), Name = "John Doe", Password = "1234", Email = "john@mail.com", EmailConfirmed = true, SecurityQuestion = "What was your nickname in college?", SecurityAnswer = "Johnny", PhoneNumber = "2347034567890" },
                new User { Id = new Guid("25AB7E48-E45C-41D7-96FB-26FECFF25A17"), Name = "Jane Doe", Password = "1234", Email = "jane@mail.com", EmailConfirmed = false, SecurityQuestion = "What is your mother's secret?", SecurityAnswer = "Nothing", PhoneNumber = "2347134567890" },
                new User { Id = new Guid("2F1DF384-5E06-4C65-9549-6D7DCA13819C"), Name = "Mc Philips", Password = "0000", Email = "mcphilips@mail.com", EmailConfirmed = true, SecurityQuestion = "What is your favorite sport?", SecurityAnswer = "Football", PhoneNumber = "2347000000000" }
            });
        }
    }
}
