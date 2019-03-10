using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public class CustomDatePicker : DatePicker
    {
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomDatePicker), "", BindingMode.TwoWay);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
    }
}
