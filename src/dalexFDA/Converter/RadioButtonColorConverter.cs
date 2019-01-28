using System;
using System.Globalization;
using Xamarin.Forms;

namespace dalexFDA
{
    public class RadioButtonColorConverter : IValueConverter
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
                retVal = "00AC00=999999";
            }
            else
            {
                retVal = "00AC00=6D9B36";
            }

            return retVal;
        }
    }
}
