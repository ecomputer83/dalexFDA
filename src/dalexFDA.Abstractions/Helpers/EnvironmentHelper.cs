using System;
namespace dalexFDA.Abstractions
{
    public class EnvironmentHelper
    {
        const string prefix = "dalexFDA.Abstractions.Configuration";

        private EnvironmentHelper()
        {

#if ENV_LOCAL_UNIT
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.localunit.xml", prefix);
#endif

#if ENV_LOCAL_MOCK
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.localmock.xml", prefix);
#endif

#if ENV_LOCAL_DEV
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.localdev.xml", prefix);
#endif

#if ENV_LOCAL_TEST
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.localtest.xml", prefix);
#endif

#if ENV_LOCAL_STAGING
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.localstaging.xml", prefix);
#endif

#if ENV_UNIT
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.unit.xml", prefix);
#endif

#if ENV_MOCK
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.mock.xml", prefix);
#endif

#if ENV_DEV
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.dev.xml", prefix);
#endif

#if ENV_TEST
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.test.xml", prefix);
#endif

#if ENV_STAGING
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.staging.xml", prefix);
#endif

#if ENV_PROD
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.prod.xml", prefix);
#endif

#if ENV_STORE
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.store.xml", prefix);
#endif

#if ENV_UITESTS
            Config = ResourceHelper.GetObject<EnvironmentConfiguration>("config.uitests.xml", prefix);
#endif

        }

        #region Lazy Instance

        private static readonly Lazy<EnvironmentHelper> lazyHelper = new Lazy<EnvironmentHelper>(() => new EnvironmentHelper());

        public static EnvironmentConfiguration Configuration
        {
            get
            {
                return lazyHelper.Value.Config;
            }
        }

        #endregion

        private EnvironmentConfiguration Config { get; set; }
    }
}
