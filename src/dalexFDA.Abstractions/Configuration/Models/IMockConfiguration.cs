using System;
namespace dalexFDA.Abstractions
{
    public interface IMockConfiguration
    {
        bool Enabled { get; }
        bool DisplayUnitTests { get; }
    }
}
