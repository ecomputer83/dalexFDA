using System;
using System.Xml.Serialization;

namespace FormsGen
{
    [Serializable]
    public class Property
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }

        [XmlAttribute]
        public bool ForListItem { get; set; }



    }
}
