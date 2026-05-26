using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Interfaces.Service
{
    public class AnimationService
    {

        public static void FadeColor(object? sender, string mode)
        {
            if (sender == null) return;

            Border border = (Border)sender;

            if (border == null) return;

            ColorAnimation fade = new ColorAnimation();

            fade.From = (Color)ColorConverter.ConvertFromString(mode == "In" ? border.Tag.ToString() : "#808080");
            fade.To = (Color)ColorConverter.ConvertFromString(mode == "In" ? "#808080" : border.Tag.ToString());
            fade.Duration = TimeSpan.FromSeconds(0.1);

            SolidColorBrush brush = new SolidColorBrush();
            border.Background = brush;

            brush.BeginAnimation(SolidColorBrush.ColorProperty, fade);

        }

    }
}
