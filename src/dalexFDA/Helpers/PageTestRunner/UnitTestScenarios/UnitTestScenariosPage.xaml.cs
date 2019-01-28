using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace dalexFDA
{
    public partial class UnitTestScenariosPage : ContentPage
    {
        public UnitTestScenariosPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            try
            {
                var list = sender as ListView;
                var vm = list.SelectedItem as UnitTestScenariosItemViewModel;
                vm.ViewPageCommand.Execute(null);
                list.SelectedItem = null;
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");

            }
        }
    }
}
