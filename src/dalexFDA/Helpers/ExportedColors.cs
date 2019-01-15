using System;
using Xamarin.Forms;

namespace Zenith
{
    public class ExportedColors
    {
        public static readonly Color PrimaryColor = Color.FromHex("#6D9B36");
        public static readonly Color AccentColor = (Color)Xamarin.Forms.Application.Current.Resources["White"];
        public static readonly Color InverseTextColor = Color.White;
    }
}
