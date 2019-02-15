using System;
using CoreAnimation;
using CoreGraphics;
using dalexFDA.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(GradientEffect), "GradientEffect")]
namespace dalexFDA.iOS
{
    public class GradientEffect : PlatformEffect
    {
        CAGradientLayer gradientLayer;

        protected override void OnAttached()
        {
            if (Element is VisualElement == false)
                return;

            SetGradient();
        }

        protected override void OnDetached()
        {
            gradientLayer?.RemoveFromSuperLayer();
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
                gradientLayer?.RemoveFromSuperLayer();

                CGRect rect;

                if (Control == null)
                {
                    //lets use container to get view dimensions
                    rect = Container.Bounds;
                }
                else
                {
                    rect = Control.Bounds;
                }

                if ((rect.Width == 0) || (rect.Height == 0))
                    return;

                var view = Element as VisualElement;

                var startColor = view.BackgroundColor;
                var endColor = dalexFDA.GradientEffect.GetGradientColor(view);

                gradientLayer = GetLinearTopDownGradientLayer(startColor.ToCGColor(), endColor.ToCGColor(), rect.Width, rect.Height);

                if (Control == null)
                {
                    Container.Layer.InsertSublayer(gradientLayer, 0);
                }
                else
                {
                    Control.Layer.InsertSublayer(gradientLayer, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot create gradient. Error: {0}", ex.Message);
            }
        }

        CAGradientLayer GetLinearTopDownGradientLayer(CGColor start, CGColor end, nfloat width, nfloat height)
        {
            var layer = new CAGradientLayer();

            layer.Frame = new CGRect(0, 0, width, height);
            layer.Colors = new CGColor[] { start, end };
            layer.StartPoint = new CGPoint(0, 0);
            layer.EndPoint = new CGPoint(0, 1);

            return layer;
        }
    }
}
