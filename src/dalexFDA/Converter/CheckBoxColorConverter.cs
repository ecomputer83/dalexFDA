using System;
using System.Globalization;
using Xamarin.Forms;

namespace dalexFDA
{
    public class CheckBoxColorConverter : IValueConverter
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
            string retVal = string.Empty;

            if (!value)
            {
                retVal = "000000=999999";
            }
            else
            {
                retVal = "000000=6D9B36";
            }

            return retVal;
        }
    }
}
