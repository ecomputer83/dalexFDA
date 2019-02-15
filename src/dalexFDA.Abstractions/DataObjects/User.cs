using System;
namespace dalexFDA.Abstractions
{
    public class User
    {
        public string ClientNo { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public Guid SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public object LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public long AccessFailedCount { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }

    public class AuthorizedAccount
    {
        public string access_token { get; set; }
    }
}
