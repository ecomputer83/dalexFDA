using System;
using System.Globalization;
using Xamarin.Forms;

namespace Zenith
{
    public class CheckBoxImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return Convert(System.Convert.ToBoolean(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string Convert(bool value)
        {
            string retVal = "res:Images.icon-checkbox";

            if (value)
                retVal = "res:Images.icon-checkbox-selected";

            return retVal;
        }
    }
}
