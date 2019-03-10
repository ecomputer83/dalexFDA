using System;
using dalexFDA.Abstractions;
using FreshMvvm;

namespace dalexFDA.iOS
{
    public static class ContainerConfig
    {

        public static void Load()
        {
            FreshIOC.Container.Register<IFileStorageService, iOSFileStorageService>();

            dalexFDA.Core.ContainerConfig.Load();
        }
    }
}
