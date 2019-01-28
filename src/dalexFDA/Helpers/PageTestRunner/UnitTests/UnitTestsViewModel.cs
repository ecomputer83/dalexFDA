using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PropertyChanged;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class UnitTestsViewModel : BaseViewModel
    {

        public ObservableCollection<UnitTestsItemViewModel> Items { get; set; }

        public UnitTestsViewModel()
        {

        }

        public override async void Init(object initData)
        {
            Items = await GetData();
        }

        async Task<ObservableCollection<UnitTestsItemViewModel>> GetData()
        {

            List<UnitTestsItemViewModel> list = new List<UnitTestsItemViewModel>();

            var types = SafeGetTypes(this.GetType().GetTypeInfo().Assembly);

            foreach (var t in types)
            {
                list.Add(new UnitTestsItemViewModel(this, t));
            }

            int count = types.Count();
            this.Title = $"{count} Pages";

            ObservableCollection<UnitTestsItemViewModel> retVal = new ObservableCollection<UnitTestsItemViewModel>(list);
            return await Task.FromResult(retVal);
        }

        public static IEnumerable<TypeInfo> SafeGetTypes(Assembly assembly)
        {
            var assemblies = assembly.DefinedTypes
                                     .Where(t => t?.BaseType?.Name?.ToLower() == "baseviewmodel" && t?.Name?.ToLower() != "unittestsviewmodel" && t?.Name?.ToLower() != "unittestscenariosviewmodel")
                                     .OrderBy(t => t.Name)
                                     ;
            return assemblies;
        }
    }
}
