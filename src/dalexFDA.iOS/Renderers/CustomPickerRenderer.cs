using System;
using dalexFDA;
using dalexFDA.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace dalexFDA.iOS
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.None;

            var customPicker = e.NewElement as CustomPicker;

            if (customPicker == null)
                return;

            // get Bindable properties
            UIColor placeholderColor = GetUIColor(customPicker.PlaceholderColor);
            float textSize = (float)customPicker.FontSize;


            // create font decsriptor
            var label = new UILabel();
            var fontDescriptor = label.Font.FontDescriptor;

            // adjusting font size
            var newDescriptor = fontDescriptor.CreateWithAttributes(new UIFontAttributes()
            {
                Size = textSize
            });
            UIFont font = UIFont.FromDescriptor(newDescriptor, 0);

            //Control.AttributedPlaceholder = new NSAttributedString(Control.AttributedPlaceholder?.Value, foregroundColor: placeholderColor);
            this.Control.Font = font;
        }

        private UIColor GetUIColor(string color)
        {
            return UIColor.FromRGB(GetRed(color), GetGreen(color), GetBlue(color));
        }

        private float GetRed(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.R;
        }

        private float GetGreen(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.G;
        }

        private float GetBlue(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.B;
        }
    }
}
