using System;
using System.Linq;
using Xamarin.Forms;

namespace dalexFDA
{
    public class NumericValidationBehavior : Behavior<Entry>
    {

        //public static readonly BindableProperty IsNumericProperty = BindableProperty.Create("IsNumeric", typeof(bool), typeof(NumericValidationBehavior), false);

        //public bool IsNumeric
        //{
        //    get { return (bool)GetValue(IsNumericProperty); }
        //    set { SetValue(IsNumericProperty, value); }
        //}

        public bool IsNumeric { get; set; }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!IsNumeric)
                return;

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x) || char.IsWhiteSpace(x) || char.IsSymbol(x)); //Make sure all characters are numbers

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }


    }
}
