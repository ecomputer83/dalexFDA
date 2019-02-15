using System;
using System.Collections.Generic;
using System.Linq;
using dalexFDA.Abstractions;
using Foundation;
using FreshMvvm;
using UIKit;

namespace dalexFDA.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        ISetting Settings;
        IEnvironmentConfiguration Config;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ContainerConfig.Load();

            var configService = FreshIOC.Container.Resolve<IConfigurationService>();
            var config = configService.Load();
            Config = config;

            Bootstrap_Init();

            global::Xamarin.Forms.Forms.Init();

            Settings = FreshIOC.Container.Resolve<ISetting>();
            Settings.StoreAndGenerateAppID();

            Bootstrap_Post_Forms_Init();

            LoadApplication(new App());

            Bootstrap_Post_Forms_LoadApp();

            return base.FinishedLaunching(app, options);
        }

        void Bootstrap_Init()
        {
            Rg.Plugins.Popup.Popup.Init();
            RegisterServices();
        }

        void Bootstrap_Post_Forms_Init()
        {
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
        }

        void Bootstrap_Post_Forms_LoadApp()
        {
            Appearance.Configure();
            XamSvg.XamForms.iOS.SvgImageRenderer.InitializeForms();

#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }

        public void RegisterServices()
        {
            //FreshIOC.Container.Register<IAuthService, AuthService>();
        }
    }
}
