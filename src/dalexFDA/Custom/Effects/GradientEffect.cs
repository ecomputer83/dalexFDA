using System;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public class GradientEffect : RoutingEffect
    {
        public GradientEffect() : base("dalexFDA.GradientEffect")
        {
        }

        public static readonly BindableProperty GradientColorProperty = BindableProperty.CreateAttached("GradientColor", typeof(Color),
                                                                                                        typeof(GradientEffect), Color.Black);

        public static void SetGradientColor(VisualElement view, Color color)
        {
            view.SetValue(GradientColorProperty, color);
        }

        public static Color GetGradientColor(VisualElement view)
        {
            return (Color)view.GetValue(GradientColorProperty);
        }
    }
}
