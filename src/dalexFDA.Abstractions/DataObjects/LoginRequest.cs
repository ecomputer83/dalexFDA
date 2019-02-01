using System;
namespace dalexFDA.Abstractions
{
    public class LoginRequest
    {
        public LoginRequest()
        {
        }

        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get { return "password"; } }
    }
}
