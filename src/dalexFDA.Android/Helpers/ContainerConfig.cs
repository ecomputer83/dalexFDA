using System;
using FreshMvvm;
using dalexFDA.Abstractions;

namespace dalexFDA.Droid
{
    public static class ContainerConfig
    {
        public static void Load()
        {
            FreshIOC.Container.Register<IFileStorageService, AndroidFileStorageService>();

            dalexFDA.Core.ContainerConfig.Load();
        }
    }
}
