using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using dalexFDA;
using dalexFDA.Droid;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace dalexFDA.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Android.Content.Context context): base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Control?.SetPadding(0, 0, 0, 0);
            if (e.OldElement != null || e.NewElement != null)
            {
                var customPicker = e.NewElement as CustomPicker;
                if (customPicker != null)
                {
                    Control.SetHintTextColor(Android.Graphics.Color.ParseColor(customPicker.PlaceholderColor));
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                var picker = sender as CustomPicker;
                Control.TextSize = (float)(picker?.FontSize ?? 14);
                Control.SetBackground(null);
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}
