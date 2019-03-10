﻿using System;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty PlaceholderTextColorProperty = BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(CustomEntry), Color.Default);

        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

        public CustomEntry()
        {
        }
    }
}
