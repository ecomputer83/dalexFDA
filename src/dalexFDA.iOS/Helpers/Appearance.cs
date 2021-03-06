﻿using System;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace dalexFDA.iOS
{
    public static class Appearance
    {
        public static UIColor PrimaryColor = ExportedColors.PrimaryColor.ToUIColor();
        public static UIColor AccentColor = ExportedColors.AccentColor.ToUIColor();
        public static UIColor TextColor = ExportedColors.InverseTextColor.ToUIColor();

        public static void Configure()
        {
            UINavigationBar.Appearance.BarTintColor = PrimaryColor;
            UINavigationBar.Appearance.TintColor = TextColor;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = TextColor,
            };

            UIProgressView.Appearance.ProgressTintColor = UIColor.White;//AccentColor;
            UIProgressView.Appearance.TrackTintColor = UIColor.LightGray;

            UISlider.Appearance.MinimumTrackTintColor = AccentColor;
            UISlider.Appearance.MaximumTrackTintColor = AccentColor;
            UISlider.Appearance.ThumbTintColor = AccentColor;

            UISwitch.Appearance.OnTintColor = AccentColor;

            //UITableViewHeaderFooterView.Appearance.TintColor = AccentColor;

            //UITableView.Appearance.SectionIndexBackgroundColor = AccentColor;
            UITableView.Appearance.SeparatorColor = AccentColor;

            UITabBar.Appearance.TintColor = AccentColor;

            //UITextField.Appearance.TintColor = UIColor.White; //AccentColor;

            UIButton.Appearance.TintColor = AccentColor;
            UIButton.Appearance.SetTitleColor(AccentColor, UIControlState.Normal);
        }
    }
}
