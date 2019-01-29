using System;
namespace dalexFDA.Abstractions
{
    public interface IEnvironmentConfiguration
    {
        string Api { get; }
        IAppCenterConfiguration AppCenter { get; }
        IPushConfiguration Push { get; }
        IMockConfiguration Mock { get; }
        string MrGestures { get; }
    }
}
