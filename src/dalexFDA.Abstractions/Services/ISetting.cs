using System;
namespace dalexFDA.Abstractions
{
    public interface ISetting
    {
        string AppID { get; set; }
        void StoreAndGenerateAppID();
    }
}
