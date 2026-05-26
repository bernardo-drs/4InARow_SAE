using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Systeme
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

        public SolidColorBrush AppliquerContraste(double sliderValue)
        {
            Color fond = sliderValue switch
            {
                2 => (Color)ColorConverter.ConvertFromString("#2d1760"),
                3 => (Color)ColorConverter.ConvertFromString("#0a0520"),
                _ => (Color)ColorConverter.ConvertFromString("#20114a")
            };
            var brush = new SolidColorBrush(fond);
            Application.Current.Resources["BackgroundColor"] = brush;
            return brush;
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
