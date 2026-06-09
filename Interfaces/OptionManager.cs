using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interfaces
{
    public class OptionManager
    {
        public void AppliquerTailleTexte(double valeur)
        {
            Application.Current.Resources["FontSize10"] = valeur switch
            {
                1 => 4.0,
                2 => 10.0,
                3 => 16.0,
                _ => 10.0
            };

            Application.Current.Resources["FontSize12"] = valeur switch
            {
                1 => 6.0,
                2 => 12.0,
                3 => 18.0,
                _ => 12.0
            };

            Application.Current.Resources["FontSize14"] = valeur switch
            {
                1 => 8.0,
                2 => 14.0,
                3 => 20.0,
                _ => 14.0
            };

            Application.Current.Resources["FontSize15"] = valeur switch
            {
                1 => 9.0,
                2 => 15.0,
                3 => 21.0, 
                _ => 15.0
            };

            Application.Current.Resources["FontSize17"] = valeur switch
            {
                1 => 11.0,
                2 => 17.0, 
                3 => 23.0,
                _ => 17.0
            };

            Application.Current.Resources["FontSize20"] = valeur switch
            {
                1 => 14.0,  
                2 => 20.0,
                3 => 26.0, 
                _ => 20.0
            };

            Application.Current.Resources["FontSize22"] = valeur switch
            {
                1 => 16.0, 
                2 => 22.0,
                3 => 28.0, 
                _ => 22.0
            };

            Application.Current.Resources["FontSize30"] = valeur switch
            {
                1 => 24.0, 
                2 => 30.0, 
                3 => 36.0, 
                _ => 30.0
            };

            Application.Current.Resources["FontSize35"] = valeur switch
            {
                1 => 29.0, 
                2 => 35.0, 
                3 => 41.0, 
                _ => 35.0
            };

            Application.Current.Resources["FontSize47"] = valeur switch
            {
                1 => 37.0, 
                2 => 47.0,
                3 => 57.0, 
                _ => 47.0
            };
        }

        public void AppliquerContraste(double valeur)
        {
            var (text, accent, buttonText, buttonText2) = valeur switch
            {
                1 => ("#FFFFFF", "#7a6fb0", "#FFFFFF", "#000000"),
                2 => ("#FFFF00", "#7a6fb0", "#FFFF00", "#FFFF00"),
                3 => ("#FFFF00", "#000000", "#FFFF00", "#FFFF00"),
                _ => ("#FFFFFF", "#7a6fb0", "#FFFFFF", "#000000")
            };

            Application.Current.Resources["TextColor"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(text));
            Application.Current.Resources["AccentColor"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(accent));
            Application.Current.Resources["ButtonTextColor"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(buttonText));
            Application.Current.Resources["ButtonTextColor2"] = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(buttonText2));

            AppliquerCouleurBoutons(valeur);
        }

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

            ConfigurationJeu.CouleurFond = hex;

            return brush;
        }

        public void AppliquerCouleurBoutons(double contraste)
        {
            if (Application.Current.MainWindow == null) return;

            Application.Current.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(() =>
                {
                    AppliquerCouleurBoutonsRecursif(Application.Current.MainWindow, contraste);
                })
            );
        }

        private void AppliquerCouleurBoutonsRecursif(DependencyObject parent, double contraste)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Border border && border.Tag is string tag && tag.StartsWith("#"))
                {
                    string couleur = contraste >= 3.0 ? "#000000" : tag;
                    border.Background = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString(couleur));
                    if (contraste >= 3.0)
                        border.Tag = "#000000";
                    border.InvalidateVisual();
                }

                AppliquerCouleurBoutonsRecursif(child, contraste);
            }
        }

        public void AppliquerFormeJeton(string forme)
        {
            ConfigurationJeu.FormeJeton = forme;
        }
    }
}
