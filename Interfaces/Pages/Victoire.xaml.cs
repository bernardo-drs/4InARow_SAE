using Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Victoire.xaml
    /// </summary>
    /// 

    public partial class Victoire : Page
    {
        public static string NomGagnant { get; set; }
        public static string CouleurGagnant { get; set; }

        public Victoire()
        {
            InitializeComponent();
            BrushConverter bc = new BrushConverter();

            RunVainqueur.Text = NomGagnant;
            Brush b = (Brush)bc.ConvertFromString(CouleurGagnant);
            RunVainqueur.Foreground = b;

            if (ConfigurationJeu.ModeDeJeu == "Challenge")
            {
                PanelJoueur1.Visibility = Visibility.Visible;
                PanelJoueur2.Visibility = Visibility.Visible;

                NomJ1.Text = ConfigurationJeu.NomJoueur1;
                NomJ2.Text = ConfigurationJeu.NomJoueur2;

                CouleurBordureJ1.Color = (Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur1);
                CouleurBordureJ2.Color = (Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur2);
            }
        }
    }
}
