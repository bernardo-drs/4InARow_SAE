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

            /*
             * 
            sender -> object Conversible en type Border
            double duration -> temp de l'animation en secondes
            string mode -> In ou Out
            string beginColor -> couleur de début d'animation si nul couleur mise dans le tag de la border
            string endColor -> couleur de fin d'animation si nul couleur mise dans le tag de la border

            penser à mettre les couleur dans le bon sens

            si mode = In couleur1 = debut couleur2 = fin
            si mode = Out couleur1 = fin couleur2 = debut

            */

            if (sender == null) return;

            Border border = (Border)sender;

            if (border == null) return;

            if (ConfigurationJeu.Contraste == 3.0)
            {
                beginColor = "#000000";
            }
            else
            {
                // On vérifie d'abord si beginColor n'a pas déjà une valeur
                if (beginColor == null)
                {
                    // On vérifie ensuite si le Tag du border n'est pas vide pour éviter le crash
                    if (border.Tag != null)
                    {
                        beginColor = border.Tag.ToString();
                    }
                    else
                    {
                        // Couleur de secours si le Tag est null (ex: blanc)
                        beginColor = "#FFFFFF";
                    }
                }
            }

            // Si contraste = 3, la couleur de fin est toujours rouge
            if (ConfigurationJeu.Contraste == 3.0)
                endColor = "#000000";
            else
                endColor = (endColor == null ? "#808080" : endColor);


            ColorAnimation fade = new ColorAnimation();

            //Debug.WriteLine($"Begin : {beginColor}, End : {endColor}");

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
