using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Interfaces
{
    public class OptionManager
    {
        // Taille du texte
        public void AppliquerTailleTexte(double valeur)
        {
            Application.Current.Resources["FontSize10"] = valeur switch
            {
                1 => 4.0,   // 10 - 6
                2 => 10.0,  // base
                3 => 16.0,  // 10 + 6
                _ => 10.0
            };

            Application.Current.Resources["FontSize12"] = valeur switch
            {
                1 => 6.0,   // 12 - 6
                2 => 12.0,  // base
                3 => 18.0,  // 12 + 6
                _ => 12.0
            };

            Application.Current.Resources["FontSize15"] = valeur switch
            {
                1 => 9.0,   // 15 - 6
                2 => 15.0,  // base
                3 => 21.0,  // 15 + 6
                _ => 15.0
            };

            Application.Current.Resources["FontSize17"] = valeur switch
            {
                1 => 11.0,  // 17 - 6
                2 => 17.0,  // base
                3 => 23.0,  // 17 + 6
                _ => 17.0
            };

            Application.Current.Resources["FontSize20"] = valeur switch
            {
                1 => 14.0,  // 20 - 6
                2 => 20.0,  // base
                3 => 26.0,  // 20 + 6
                _ => 20.0
            };

            Application.Current.Resources["FontSize22"] = valeur switch
            {
                1 => 16.0,  // 22 - 6
                2 => 22.0,  // base
                3 => 28.0,  // 22 + 6
                _ => 22.0
            };

            Application.Current.Resources["FontSize30"] = valeur switch
            {
                1 => 24.0,  // 30 - 6
                2 => 30.0,  // base
                3 => 36.0,  // 30 + 6
                _ => 30.0
            };

            Application.Current.Resources["FontSize35"] = valeur switch
            {
                1 => 29.0,  // 35 - 6
                2 => 35.0,  // base
                3 => 41.0,  // 35 + 6
                _ => 35.0
            };

            Application.Current.Resources["FontSize47"] = valeur switch
            {
                1 => 37.0,  // 47 - 10
                2 => 47.0,  // base
                3 => 57.0,  // 47 + 10
                _ => 47.0
            };
        }

        // Contraste
        public void AppliquerContraste(double valeur)
        {
            // Valeur 1 = normal, 2 = moyen, 3 = fort
            var (bg, text, accent) = valeur switch
            {
                1 => ("#20114a", "#FFFFFF", "#7a6fb0"),
                2 => ("#000000", "#FFFFFF", "#9988FF"),
                3 => ("#000000", "#FFFF00", "#FF0000"),
                _ => ("#20114a", "#FFFFFF", "#7a6fb0")
            };

            Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(bg));
            Application.Current.Resources["TextColor"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(text));
            Application.Current.Resources["AccentColor"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(accent));
        }

        // Couleur de fond
        public SolidColorBrush? AppliquerCouleurFond(string nomCouleur)
        {
            string hex = nomCouleur switch
            {
                "CouleurBleuFonce" => "#20114a",
                "CouleurMauve" => "#81608a",
                "CouleurVert" => "#01773e",
                "CouleurOrange" => "#a7501a",
                "CouleurJaune" => "#ccb147",
                "CouleurBleuClair" => "#2579a9",
                _ => "#20114a"
            };

            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
            Application.Current.Resources["BackgroundColor"] = brush;

            // Sauvegarde le hex dans ConfigurationJeu
            ConfigurationJeu.CouleurFond = hex;

            return brush;
        }

        // Forme des Jetons
        public void AppliquerFormeJeton(string forme)
        {
            // Stocke la forme — utilisée par la grille de jeu pour dessiner les jetons
            ConfigurationJeu.FormeJeton = forme;
        }
    }
}
