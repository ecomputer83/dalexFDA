using System;
using dalexFDA;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(RemoveBorderEffect), "RemoveBorderEffect")]
namespace dalexFDA.Droid
{
    public class RemoveBorderEffect : PlatformEffect
    {
        public RemoveBorderEffect()
        {
        }

        Android.Graphics.Color backgroundColor;

        protected override void OnAttached()
        {
            try
            {
                if (Control != null)
                {
                    Control.SetBackground(null);
                    Control.SetPadding(0, 0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during OnAttach of RemoveBorderEffect - {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during OnDetach of RemoveBorderEffect - {0}", ex.Message);
            }
        }
    }
}
