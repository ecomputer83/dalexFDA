using System;
namespace dalexFDA.Abstractions
{
    public interface IConfigurationService
    {
        IEnvironmentConfiguration Current { get; }
        IEnvironmentConfiguration Load();
    }
}
