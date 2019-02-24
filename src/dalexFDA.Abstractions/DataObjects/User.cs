﻿using System;
namespace dalexFDA.Abstractions
{
    public class User
    {
        public string ClientNo { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Status { get; set; }

        public string PostalAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string CopyOfValidId { get; set; }
        public string ProofOfResUtilityBill { get; set; }
        public string Nationality { get; set; }
        public string HomeTown { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime ExpiryDateOfId { get; set; }

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
