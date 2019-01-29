using System;
namespace dalexFDA.Abstractions
{
    public interface IFileStorageService
    {
        string ReadAsString(string fileName);
    }
}
