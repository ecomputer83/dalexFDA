using System;
using System.Xml.Serialization;

namespace FormsGen
{
    [Serializable]
    public class PageService
    {
        [XmlAttribute]
        public string Name { get; set; }

    }
}
