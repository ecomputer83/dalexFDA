using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace FormsGen
{
    [Serializable]
    public class Page
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Folder { get; set; }
        public PageType PageType { get; set; }

        [XmlArray("Commands")]
        [XmlArrayItem("Command")]
        public List<Command> Commands { get; set; } = new List<Command>();

        [XmlArray("Properties")]
        [XmlArrayItem("Property")]
        public List<Property> Properties { get; set; } = new List<Property>();

        [XmlArray("Services")]
        [XmlArrayItem("Service")]
        public List<PageService> Services { get; set; }

        public Service GetService(Project project, PageService pageService)
        {
            Service service = project.Services
                                     .Where(s => s.Name.ToLower() == pageService.Name.ToLower())
                                     .SingleOrDefault();

            return service;
        }
    }
}
