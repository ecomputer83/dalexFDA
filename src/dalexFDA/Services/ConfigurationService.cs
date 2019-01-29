using System;
using dalexFDA.Abstractions;
using dalexFDA.Abstractions.Configuration;
using Newtonsoft.Json;

namespace dalexFDA
{
    public class ConfigurationService : IConfigurationService
    {
        readonly IFileStorageService FileStorage;

        public ConfigurationService(IFileStorageService fileStorageService)
        {
            FileStorage = fileStorageService;
        }

        public IEnvironmentConfiguration Current { get; set; }

        public IEnvironmentConfiguration Load()
        {
            var configJson = FileStorage.ReadAsString("config.common.json");

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new EnvironmentConfigurationConverter());

            var configuration = JsonConvert.DeserializeObject<EnvironmentConfiguration>(configJson, settings);

            Current = configuration;
            return configuration;
        }

        class EnvironmentConfigurationConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                if (typeof(IAppCenterConfiguration) == objectType)
                    return true;

                if (typeof(IPushConfiguration) == objectType)
                    return true;

                if (typeof(IMockConfiguration) == objectType)
                    return true;

                return false;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (typeof(IAppCenterConfiguration) == objectType)
                    return serializer.Deserialize(reader, typeof(AppCenterConfiguration));

                if (typeof(IPushConfiguration) == objectType)
                    return serializer.Deserialize(reader, typeof(PushConfiguration));

                if (typeof(IMockConfiguration) == objectType)
                    return serializer.Deserialize(reader, typeof(MockConfiguration));

                return null;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (typeof(IAppCenterConfiguration) == value.GetType())
                    serializer.Serialize(writer, value, typeof(AppCenterConfiguration));

                if (typeof(IPushConfiguration) == value.GetType())
                    serializer.Serialize(writer, value, typeof(PushConfiguration));

                if (typeof(IMockConfiguration) == value.GetType())
                    serializer.Serialize(writer, value, typeof(MockConfiguration));
            }
        }
    }
}
