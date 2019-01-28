using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace dalexFDA
{
    public partial class UnitTestsPage : ContentPage
    {
        public UnitTestsPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            try
            {
                var list = sender as ListView;
                UnitTestsItemViewModel vm = list.SelectedItem as UnitTestsItemViewModel;
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
