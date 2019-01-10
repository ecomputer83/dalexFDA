using System;
using Zenith;
using Zenith.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace Zenith.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                var picker = sender as CustomPicker;
                Control.TextSize = picker?.FontSize ?? 14;
                Control.SetBackground(null);
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}
