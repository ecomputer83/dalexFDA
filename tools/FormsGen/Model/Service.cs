using System;
using System.Xml.Serialization;

namespace FormsGen
{
    [Serializable]
    public class Service
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string FullTypeName { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public bool IsDefault { get; set; }


    }
}
