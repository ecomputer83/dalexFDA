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
            var user = users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user != null)
                user.PhoneNumberConfirmed = true;

            var retVal = code == "0000";
            return await Task.FromResult(retVal);
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
            var user = users.FirstOrDefault(x => x.PhoneNumber == $"{phoneExtension}{phoneNumber}");
            return await Task.FromResult(user);
        }

        public async Task<SignupResponse> Signup(SignupRequest data)
        {
            await Task.Delay(1000);
            var retVal = new SignupResponse();
            CreateUser(data);
            return await Task.FromResult(retVal);
        }

        public async Task<SignupResponse> SignupExistingUser(SignupRequest data)
        {
            await Task.Delay(800);
            var retVal = new SignupResponse();
            CreateUser(data);
            return await Task.FromResult(retVal);
        }

        public async Task ResetPin(ResetPinRequest request)
        {
            bool retVal = true;
            var user = users.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
            if(user != null)
            {
                user.Password = request.NewPassword;
            }
            await Task.FromResult(retVal);
            //return 
        }

        public async Task<bool> UpdateKYCAccount(KYCProfileRequest request)
        {
            bool retVal = true;
            
            return await Task.FromResult(retVal);
        }

        public async Task<string> AddDocument(DocumentRequest request)
        {
            await Task.Delay(500);
            var retVal = "something";
            return await Task.FromResult(retVal);
        }

        void CreateUser(SignupRequest data)
        {
            User user = new User
            {
                Name = data.Name,
                Password = data.Password,
                Email = data.Email,
                EmailConfirmed = false,
                SecurityQuestion = data.SecurityQuestion,
                SecurityAnswer = data.SecurityAnswer,
                PhoneNumber = data.PhoneNumber,
                PhoneNumberConfirmed = false
            };
            var existingUser = users.FirstOrDefault(x => x.PhoneNumber == data.PhoneNumber);

            if (existingUser != null)
            {
                existingUser = user;
            }
            else
            {                
                users.Add(user);
            }
        }

        void SetupUsers()
        {
            users.AddRange(new List<User>
            {
                new User { Id = "32999016-4DA7-409D-8D5E-F72736DAD00D", Name = "John Doe", Password = "1234", Email = "john@mail.com", EmailConfirmed = true, SecurityQuestion = "What was your nickname in college?", SecurityAnswer = "Johnny", PhoneNumber = "2347034567890", Address = "123, Test Avenue, Ghana", ClientNo = "9187402" },
                new User { Id = "25AB7E48-E45C-41D7-96FB-26FECFF25A17", Name = "Jane Doe", Password = "1234", Email = "jane@mail.com", EmailConfirmed = false, SecurityQuestion = "What is your mother's secret?", SecurityAnswer = "Nothing", PhoneNumber = "2347134567890", Address = "234, Test Avenue, Ghana", ClientNo = "019201931" },
                new User { Id = "2F1DF384-5E06-4C65-9549-6D7DCA13819C", Name = "Mc Philips", Password = "0000", Email = "mcphilips@mail.com", EmailConfirmed = true, SecurityQuestion = "What is your favorite sport?", SecurityAnswer = "Football", PhoneNumber = "2347000000000", Address = "100, Test Avenue, Ghana", ClientNo = "1897092" }
            });
        }

        public Task<KYCApplication> GetApplication()
        {
            return Task.FromResult(new KYCApplication());
        }
    }
}
