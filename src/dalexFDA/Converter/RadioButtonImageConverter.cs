using System;
using System.Globalization;
using Xamarin.Forms;

namespace dalexFDA
{
    public class RadioButtonImageConverter : IValueConverter
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
            string retVal = "res:Images.icon-unselected";

            if (value)
                retVal = "res:Images.icon-selected";

            return retVal;
        }
    }
}
