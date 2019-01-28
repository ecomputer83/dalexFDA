using System;
using Xamarin.Forms;

namespace dalexFDA
{
    public class DatePickerCtrl : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(DatePickerCtrl), defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }
    }
}
