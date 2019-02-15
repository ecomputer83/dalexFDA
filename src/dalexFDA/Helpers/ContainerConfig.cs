using System;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using FreshMvvm;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace dalexFDA
{
    public static class ContainerConfig
    {
        public static void Load()
        {
            FreshIOC.Container.Register<IConfigurationService, ConfigurationService>();
            FreshIOC.Container.Register<ISession, SessionService>();
            FreshIOC.Container.Register<ISetting, Settings>();
        }
    }
}
