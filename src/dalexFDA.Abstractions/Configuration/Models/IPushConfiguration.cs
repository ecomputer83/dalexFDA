using System;
namespace dalexFDA.Abstractions
{
    public interface IPushConfiguration
    {
        string AppId { get; }
        string Permission { get; }
        string ProjectId { get; }
        string Key { get; }
    }
}
