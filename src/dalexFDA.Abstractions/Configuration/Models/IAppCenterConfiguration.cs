using System;
namespace dalexFDA.Abstractions
{
    public interface IAppCenterConfiguration
    {
        string iOS { get; }
        string Droid { get; }
    }
}
