using System;
using System.Xml.Serialization;

namespace FormsGen
{
    [Serializable]
    public class Command
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public bool ForListItem { get; set; }



    }
}
