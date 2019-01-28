using System;
using System.ComponentModel;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using dalexFDA;
using dalexFDA.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace dalexFDA.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //Control.BorderStyle = UITextBorderStyle.None;
            //Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
            //Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
            //Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "PlaceholderTextColor")
            {
                var view = (CustomEntry)Element;

                if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderTextColor != Color.Default)
                {
                    NSAttributedString placeholderString = new NSAttributedString(view.Placeholder, new UIStringAttributes() { ForegroundColor = view.PlaceholderTextColor.ToUIColor() });
                    Control.AttributedPlaceholder = placeholderString;
                }
            }
        }
    }
}
