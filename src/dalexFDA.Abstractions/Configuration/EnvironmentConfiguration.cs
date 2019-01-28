using System;
namespace dalexFDA.Abstractions
{
    public class EnvironmentConfiguration
    {
        public EnvironmentType Environment { get; set; }
        public bool IsInScreenUnitTestingMode { get; set; }
        public string ServiceUrl { get; set; }
        public bool UsesMockData { get; set; }
        public string AppCenteriOSKey { get; set; }
        public string AppCenterDroidKey { get; set; }
    }
}
