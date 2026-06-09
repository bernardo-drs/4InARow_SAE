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

        public static void FadeColor(object? sender, double duration, string mode, string? beginColor, string? endColor)
        {


            if (sender == null) return;

            Border border = (Border)sender;

            if (border == null) return;

            if (ConfigurationJeu.Contraste == 3.0)
            {
                beginColor = "#000000";
            }
            else
            {
                if (beginColor == null)
                {
                    if (beginColor == null)
                    {
                        if (border.Tag != null)
                            beginColor = border.Tag.ToString();
                        else
                            beginColor = "#FFFFFF";
                    }
                }
            }

            if (ConfigurationJeu.Contraste == 3.0)
                endColor = "#000000";
            else
                endColor = (endColor == null ? "#808080" : endColor);


            ColorAnimation fade = new ColorAnimation();


            try
            {
                fade.From = (Color)ColorConverter.ConvertFromString(mode == "In" ? beginColor : endColor);
                fade.To = (Color)ColorConverter.ConvertFromString(mode == "Out" ? beginColor : endColor);
                fade.Duration = TimeSpan.FromSeconds(duration);

                SolidColorBrush brush = (SolidColorBrush)border.Background;

                brush.BeginAnimation(SolidColorBrush.ColorProperty, fade);
            } 
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }


        }

    }
}
