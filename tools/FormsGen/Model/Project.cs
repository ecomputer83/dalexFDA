using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FormsGen
{
    [Serializable]
    public class Project
    {
        [XmlAttribute]
        public string Namespace { get; set; }

        [XmlAttribute]
        public int SchemaVersion { get; set; }

        [XmlArray("Pages")]
        [XmlArrayItem("Page")]
        public List<Page> Pages { get; set; } = new List<Page>();

        [XmlArray("Services")]
        [XmlArrayItem("Service")]
        public List<Service> Services { get; set; } = new List<Service>();

    }
}
