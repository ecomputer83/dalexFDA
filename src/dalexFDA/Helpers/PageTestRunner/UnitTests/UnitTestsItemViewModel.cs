using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class UnitTestsItemViewModel
    {
        public string Name { get; set; }

        readonly UnitTestsViewModel Parent;
        readonly TypeInfo PageType;

        public UnitTestsItemViewModel(UnitTestsViewModel parent, TypeInfo pageType)
        {
            this.Parent = parent;
            this.PageType = pageType;

            this.Name = Regex.Replace(pageType.Name.Replace("ViewModel", ""), @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
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

                var scenarios = PageScenarioSetup.Scenarios.Where(s => s.PageType.Name == this.PageType.Name);

                var core = Parent.CoreMethods;
                var methods = core.GetType().GetRuntimeMethods()
                              .Where(m => m.Name == "PushPageModel" && m.ContainsGenericParameters == false && m.GetParameters().Length == 4 && m.GetParameters()[0].Name == "pageModelType");

                if (methods.Count() > 0)
                {
                    MethodInfo method = methods.FirstOrDefault();
                    object[] parameters = null;
                    PageScenario scenario = null;

                    if (method != null)
                    {

                        if (scenarios.Count() > 0)
                        {
                            //naviagate to scenarios page
                            if (scenarios.Count() == 1)
                            {
                                //navigate to page --pass scenario details

                                scenario = scenarios.FirstOrDefault();

                                parameters = new object[]
                                {
                                            scenario.PageType,
                                            scenario.Data,
                                            scenario.Modal,
                                            scenario.Animate
                                };

                                if (scenario.Setup != null)
                                {
                                    scenario.Setup();
                                }

                                await (Task)method.Invoke(core, parameters);
                            }
                            else
                            {
                                //navigate to scenario listing

                                var nav = new UnitTestScenariosViewModel.Nav { Scenarios = scenarios.ToList() };

                                await Parent.CoreMethods.PushPageModel<UnitTestScenariosViewModel>(nav);

                            }


                        }
                        else
                        {
                            //navigate to page - don't pass any data - use default settings
                            scenario = new PageScenario(this.PageType.AsType(), "default");

                            parameters = new object[]
                            {
                                        scenario.PageType,
                                        scenario.Data,
                                        scenario.Modal,
                                        scenario.Animate
                            };

                            await (Task)method.Invoke(core, parameters);
                        }

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
