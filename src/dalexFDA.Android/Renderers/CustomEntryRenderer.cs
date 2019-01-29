using System;
using System.ComponentModel;
using Android.Graphics.Drawables;
using dalexFDA;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using dalexFDA.Droid;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace dalexFDA.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var padding = 0;

            this.Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
            this.Control.SetPadding(padding, padding, padding, padding);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "PlaceholderTextColor")
            {
                var view = (CustomEntry)Element;

                if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderTextColor != Color.Default)
                {
                    Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
                }
            }
        }
    }
}
