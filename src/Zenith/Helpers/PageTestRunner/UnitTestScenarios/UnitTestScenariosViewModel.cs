using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PropertyChanged;

namespace Zenith
{
    [AddINotifyPropertyChangedInterface]
    public class UnitTestScenariosViewModel : BaseViewModel
    {


        public ObservableCollection<UnitTestScenariosItemViewModel> Items { get; set; }

        public UnitTestScenariosViewModel()
        {

        }

        public class Nav
        {
            public List<PageScenario> Scenarios { get; set; }
        }


        public override async void Init(object initData)
        {
            var data = initData as Nav;
            if (data != null)
            {
                if (data.Scenarios.Count > 0)
                {
                    this.Title = Regex.Replace(data.Scenarios[0].PageType.Name.Replace("ViewModel", ""), @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
                }
                Items = await GetData(data.Scenarios);
            }
        }

        async Task<ObservableCollection<UnitTestScenariosItemViewModel>> GetData(List<PageScenario> Scenarios)
        {


            List<UnitTestScenariosItemViewModel> list = new List<UnitTestScenariosItemViewModel>();


            foreach (var s in Scenarios)
            {
                list.Add(new UnitTestScenariosItemViewModel(this, s));
            }

            ObservableCollection<UnitTestScenariosItemViewModel> retVal = new ObservableCollection<UnitTestScenariosItemViewModel>(list);
            return await Task.FromResult(retVal);
        }
    }
}
