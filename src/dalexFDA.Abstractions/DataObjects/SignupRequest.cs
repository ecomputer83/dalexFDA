using System;
namespace dalexFDA.Abstractions
{
    public class SignupRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public MobileDevice MobileDevice { get; set; }
    }

    public class MobileDevice
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceVendorId { get; set; }
        public string DeviceModel { get; set; }
        public string PushNotificationService { get; set; }
        public string PushNotificationAppId { get; set; }
        public string PushNotificationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreateOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public long Status { get; set; }
    }
}
