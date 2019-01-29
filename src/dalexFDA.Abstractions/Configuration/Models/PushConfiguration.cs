using System;
namespace dalexFDA.Abstractions
{
    public class PushConfiguration : IPushConfiguration
    {
        public string AppId { get; set; }
        public string Permission { get; set; }
        public string ProjectId { get; set; }
        public string Key { get; set; }
    }
}
