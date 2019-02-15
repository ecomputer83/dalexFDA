using System;
using FreshMvvm;
using dalexFDA.Abstractions;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;

namespace dalexFDA.Droid
{
    public static class ContainerConfig
    {
        public static void Load()
        {
            //FreshIOC.Container.Register<IDeviceInfo>(CrossDeviceInfo.Current);
            FreshIOC.Container.Register<IFileStorageService, AndroidFileStorageService>();

            dalexFDA.ContainerConfig.Load();
        }
    }
}
