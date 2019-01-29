using System;

namespace dalexFDA.Abstractions.Configuration
{
    public class EnvironmentConfiguration : IEnvironmentConfiguration
    {
        public string Api { get; set; }
        public IAppCenterConfiguration AppCenter { get; set; }
        public IPushConfiguration Push { get; set; }
        public IMockConfiguration Mock { get; set; }
        public string MrGestures { get; set; }

    }
}
