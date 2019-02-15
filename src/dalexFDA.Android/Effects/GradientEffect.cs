using System;
using System.Diagnostics;
using Android.Graphics.Drawables;
using dalexFDA.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(GradientEffect), "GradientEffect")]
namespace dalexFDA.Droid
{
    public class GradientEffect : PlatformEffect
    {
        Drawable oldDrawable;

        protected override void OnAttached()
        {
            if (Element is VisualElement == false)
                return;

            Debug.WriteLine($"..............Control Background = {Control?.Background}.......................");
            Debug.WriteLine($"..............Container Background = {Container?.Background}.......................");

            SetGradient();
        }

        protected override void OnDetached()
        {
            //SetGradient();

        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == dalexFDA.GradientEffect.GradientColorProperty.PropertyName)
            {
                SetGradient();
            }
        }

        private void SetGradient()
        {
            try
            {
                var view = Element as VisualElement;

                var startColor = view.BackgroundColor;
                var endColor = dalexFDA.GradientEffect.GetGradientColor(view);

                var background = GetLinearTopDownGradient(startColor.ToAndroid(), endColor.ToAndroid());

                if (Control == null)
                    Container.SetBackground(background);
                else
                    Control.SetBackground(background);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot create gradient. Error: {0}", ex.Message);
            }
        }

        GradientDrawable GetLinearTopDownGradient(Android.Graphics.Color start, Android.Graphics.Color end)
        {
            return new GradientDrawable(GradientDrawable.Orientation.TlBr, new int[] { start, start, start, end });
        }
    }
}
