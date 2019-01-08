using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace Zenith
{
    [AddINotifyPropertyChangedInterface]
    public class UnitTestScenariosItemViewModel
    {
        public string Name { get; set; }

        readonly UnitTestScenariosViewModel Parent;
        readonly PageScenario Scenario;

        public UnitTestScenariosItemViewModel(UnitTestScenariosViewModel parent, PageScenario scenario)
        {
            this.Parent = parent;
            this.Scenario = scenario;

            this.Name = scenario.Name;
        }

        #region Command - ViewPageCommand

        private Command _ViewPageCommand;
        public const string ViewPageCommandPropertyName = "ViewPageCommand";

        public Command ViewPageCommand
        {
            get
            {
                return _ViewPageCommand ?? (_ViewPageCommand = new Command(async () => await ExecuteViewPageCommand()));
            }
        }

        protected async Task ExecuteViewPageCommand()
        {
            try
            {
                var core = Parent.CoreMethods;
                var methods = core.GetType().GetRuntimeMethods()
                              .Where(m => m.Name == "PushPageModel" && m.ContainsGenericParameters == false && m.GetParameters().Length == 4 && m.GetParameters()[0].Name == "pageModelType");

                if (methods.Count() > 0)
                {
                    MethodInfo method = methods.FirstOrDefault();
                    object[] parameters = null;


                    if (method != null)
                    {



                        parameters = new object[]
                        {
                                            Scenario.PageType,
                                            Scenario.Data,
                                            Scenario.Modal,
                                            Scenario.Animate
                        };

                        if (Scenario.Setup != null)
                        {
                            Scenario.Setup();
                        }

                        await (Task)method.Invoke(core, parameters);
                    }
                }
            }
            catch (Exception ex)
            {
                await Parent.CoreMethods.DisplayAlert("error", ex.Message, "ok");
            }

        }

        #endregion
    }
}
