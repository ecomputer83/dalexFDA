using System;
using dalexFDA.Abstractions;

namespace dalexFDA.Core
{
    public class SessionService : ISession
    {
        public SessionService()
        {
        }

        public string Token { get; set; }
        public User CurrentUser { get; set; }
    }
}
