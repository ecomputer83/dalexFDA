using System;
namespace dalexFDA.Abstractions
{
    public class AppCenterConfiguration : IAppCenterConfiguration
    {
        public string iOS { get; set; }

        public string Droid { get; set; }
    }
}
