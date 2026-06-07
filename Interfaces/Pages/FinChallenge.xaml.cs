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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Interfaces.Service;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour FinChallenge.xaml
    /// </summary>
    public partial class FinChallenge : Page
    {
        public static string NomVainqueur { get; set; }
        public static string CouleurVainqueur { get; set; }
        public static string NomPerdant { get; set; }
        public static string CouleurPerdant { get; set; }

        public FinChallenge()
        {
            InitializeComponent();

            RunVainqueur.Text = NomVainqueur;
            RunVainqueur.Foreground = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(CouleurVainqueur));

            RunPerdant.Text = NomPerdant;
            RunPerdant.Foreground = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(CouleurPerdant));

            RunNbVictoires.Text = ConfigurationJeu.VictoiresRequises.ToString();

            BordureJ1.Color = (Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur1);
            BordureJ2.Color = (Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur2);
            NomJ1.Text = ConfigurationJeu.NomJoueur1;
            NomJ2.Text = ConfigurationJeu.NomJoueur2;
            ScoreJ1.Text = ConfigurationJeu.ScoreJoueur1.ToString();
            ScoreJ2.Text = ConfigurationJeu.ScoreJoueur2.ToString();

            PanelJoueur1.Visibility = Visibility.Visible;
            PanelJoueur2.Visibility = Visibility.Visible;
        }

        private void BtnRejouer_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationJeu.ScoreJoueur1 = 0;
            ConfigurationJeu.ScoreJoueur2 = 0;
            PageService.Navigate("Game");
            PageService.PopUp(null);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationJeu.ScoreJoueur1 = 0;
            ConfigurationJeu.ScoreJoueur2 = 0;
            PageService.PopUp(null);
            PageService.Navigate("Accueil");
            ConfigurationJeu.ModeDeJeu = "Classique";
        }
    }
}
