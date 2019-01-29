using System;
namespace dalexFDA.Abstractions
{
    public class MockConfiguration : IMockConfiguration
    {
        public bool Enabled { get; set; }
        public bool DisplayUnitTests { get; set; }
    }
}
