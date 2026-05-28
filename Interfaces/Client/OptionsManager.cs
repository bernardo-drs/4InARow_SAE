using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Interfaces
{
    public class OptionManager
    {
        private readonly Dictionary<string, string> _couleursFond = new()
        {
            { "CouleurBleuFonce",  "#20114a" },
            { "CouleurMauve",  "#81608a" },
            { "CouleurVert",   "#01773e" },
            { "CouleurOrange", "#a7501a" },
            { "CouleurJaune",  "#ccb147" },
            { "CouleurBleuClair",   "#2579a9" },
        };

        public void AppliquerTailleTexte(double sliderValue)
        {
            double taille = sliderValue switch
            {
                1 => 20,
                3 => 40,
                _ => 30
            };
            Application.Current.Resources["GlobalFontSize"] = taille;
        }

        public void AppliquerContraste(double sliderValue)
        {
            switch (sliderValue)
            {
                case 1:
                    Application.Current.Resources["TextColor"] = new SolidColorBrush(Colors.White);
                    Application.Current.Resources["AccentColor"] = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString("#8176a8"));
                    Application.Current.Resources["ButtonTextColor"] = new SolidColorBrush(Colors.Black);
                    break;

                case 2:
                    Application.Current.Resources["TextColor"] = new SolidColorBrush(Colors.Yellow);
                    Application.Current.Resources["AccentColor"] = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString("#a020f0"));
                    Application.Current.Resources["ButtonTextColor"] = new SolidColorBrush(Colors.White);
                    break;

                case 3:
                    Application.Current.Resources["TextColor"] = new SolidColorBrush(Colors.Black);
                    Application.Current.Resources["AccentColor"] = new SolidColorBrush(Colors.Black);
                    Application.Current.Resources["ButtonTextColor"] = new SolidColorBrush(Colors.White);
                    break;
            }
        }

        public SolidColorBrush? AppliquerCouleurFond(string nomBouton)
        {
            if (!_couleursFond.TryGetValue(nomBouton, out string? hex))
                return null;
            var couleur = (Color)ColorConverter.ConvertFromString(hex);
            var brush = new SolidColorBrush(couleur);
            Application.Current.Resources["ChosenBackgroundColor"] = brush;
            return brush;
        }

        public void AppliquerFormeJeton(string forme)
        {
            Application.Current.Resources["FormeJeton"] = forme;
        }
    }
}