using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Zenith.Abstractions
{
    public static class ResourceHelper
    {
        public static string GetContents(string resourcePath, string prefix = null)
        {
            string retVal = null;

            var resourceName = GetResourceName(resourcePath, prefix);
            var assembly = typeof(ResourceHelper).GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        retVal = reader.ReadToEnd();
                    }
                }
                else
                {
                    throw new Exception($"Unable to find resource - {resourceName}");
                }
            }

            return retVal;
        }

        private static XDocument GetXDocument(string resourceName)
        {
            XDocument retVal = null;


            var assembly = typeof(ResourceHelper).GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    retVal = XDocument.Load(stream);
                }
                else
                {
                    throw new Exception($"Unable to find resource - {resourceName}");
                }
            }

            return retVal;
        }

        public static T GetObject<T>(string resourcePath, string prefix = null)
        {
            T retVal = default(T);

            var resourceName = GetResourceName(resourcePath, prefix);
            var doc = GetXDocument(resourceName);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            System.Xml.XmlReader reader = doc.CreateReader();
            reader.MoveToContent();

            try
            {
                retVal = (T)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while processing resource - {resourceName} - {ex.Message}", ex);
            }

            return retVal;
        }

        private static string GetResourceName(string resourcePath, string prefix = null)
        {
            if (String.IsNullOrEmpty(prefix))
            {
                return $"{resourcePath}";
            }
            else
            {
                return $"{prefix}.{resourcePath}";
            }
        }
    }
}
