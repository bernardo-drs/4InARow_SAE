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
    /// Interaction logic for Egalite.xaml
    /// </summary>
    public partial class Egalite : Page
    {
        public Egalite()
        {
            InitializeComponent();

            BordureJ1.Color = (Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur1);
            BordureJ2.Color = (Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur2);

            NomJ1.Text = ConfigurationJeu.NomJoueur1;
            NomJ2.Text = ConfigurationJeu.NomJoueur2;

            ScoreJ1.Text = ConfigurationJeu.ScoreJoueur1.ToString();
            ScoreJ2.Text = ConfigurationJeu.ScoreJoueur2.ToString();


            if (ConfigurationJeu.ModeDeJeu == "Challenge")
            {
                PanelJoueur1.Visibility = Visibility.Visible;
                PanelJoueur2.Visibility = Visibility.Visible;

                BordureBtnRejouer.Visibility = Visibility.Collapsed;
                BordureBtnMenu.Visibility = Visibility.Collapsed;

                BordureBtnContinuer.Visibility = Visibility.Visible;

            }

            this.Loaded += (s, e) => ContrasteService.AppliquerContraste(this);
            this.IsVisibleChanged += (s, e) => ContrasteService.AppliquerContraste(this);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            PageService.Navigate("Accueil");
            PageService.PopUp(null);

            ConfigurationJeu.ScoreJoueur1 = 0;
            ConfigurationJeu.ScoreJoueur2 = 0;

        }

        private void btn_Rejouer_Click(object sender, RoutedEventArgs e)
        {
            PageService.PopUp(null);
            PageService.Navigate("Game");

        }

        private void revoir_grille_Click(object sender, RoutedEventArgs e)
        {
            PageService.PopUp(null);
        }
    }
}
