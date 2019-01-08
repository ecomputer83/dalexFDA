using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Runtime;
using Effects.Droid;
using System;

[assembly: ExportEffect(typeof(RemoveBorderEffect), nameof(RemoveBorderEffect))]
namespace Effects.Droid
{
    [Preserve(AllMembers = true)]
    public class RemoveBorderEffect : PlatformEffect
    {
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
                Console.WriteLine("Error during OnAttach of NoBorderStyleEffect - {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during OnDetach of NoBorderStyleEffect - {0}", ex.Message);
            }
        }
    }
}
