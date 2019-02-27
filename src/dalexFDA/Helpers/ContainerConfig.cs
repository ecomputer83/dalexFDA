using System;
using dalexFDA.Abstractions;
using FreshMvvm;
using Plugin.Media;
using Plugin.Media.Abstractions;

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
