using System;
namespace dalexFDA
{
    public class PageScenario
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
        public object Data { get; set; }
        public bool Modal { get; set; } = false;
        public bool Animate { get; set; } = true;
        public Action Setup { get; set; }

        public PageScenario()
        {

        }

        public PageScenario(Type pageType, string name, object data = null, bool modal = false, bool animate = true, Action setup = null)
        {
            this.PageType = pageType;
            this.Name = name;
            this.Data = data;
            this.Modal = modal;
            this.Animate = animate;
            this.Setup = setup;
        }
    }
}
