using System;
using Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(RemoveBorderEffect), "RemoveBorderEffect")]
namespace dalexFDA.iOS
{
    public class RemoveBorderEffect : PlatformEffect
    {
        UITextBorderStyle old;

        protected override void OnAttached()
        {
            var editText = Control as UITextField;
            if (editText == null)
                return;

            old = editText.BorderStyle;
            editText.BorderStyle = UITextBorderStyle.None;
        }

        protected override void OnDetached()
        {
            var editText = Control as UITextField;
            if (editText == null)
                return;

            editText.BorderStyle = old;
        }
    }
}
