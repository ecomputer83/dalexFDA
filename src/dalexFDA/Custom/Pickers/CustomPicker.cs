using System;
using Xamarin.Forms;

namespace dalexFDA
{
    public class CustomPicker : Picker
    {
        public CustomPicker()
        {
        }

        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(int), typeof(CustomPicker), defaultValue: 14, defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (newValue != null && newValue is int)
                    {
                        var picker = bindable as CustomPicker;
                        picker.FontSize = (int)newValue;
                    }
                });
    }
}
