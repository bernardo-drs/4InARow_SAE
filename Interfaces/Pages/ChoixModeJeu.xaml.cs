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

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour ChoixModeJeu.xaml
    /// </summary>
    public partial class ChoixModeJeu : Page
    {
        private bool modeChallenge = false; // mode de jeu classique par défaut
        private int nbVictoires = 0;
        public ChoixModeJeu()
        {
            InitializeComponent();
        }

        private void btnChallenge_Button_Click(object sender, RoutedEventArgs e)
        {
            modeChallenge = true;

            var bc = new BrushConverter();
            var couleurHexa = (Brush)bc.ConvertFrom("#20114A");

            brd3V.Background = couleurHexa;
            brd5V.Background = couleurHexa;
            btn3V.IsEnabled = true;
            btn5V.IsEnabled = true;

            brdChall.BorderThickness = new Thickness(2);
            brdClass.BorderThickness = new Thickness(0);

            Interfaces.ConfigurationJeu.ModeDeJeu = "Challenge";

        }

        private void btnClassique_Click(object sender, RoutedEventArgs e)
        {
            modeChallenge = false;
            nbVictoires = 0;

            var bc = new BrushConverter();
            var couleurHexa = (Brush)bc.ConvertFrom("#605786");

            brd3V.Background = couleurHexa;
            brd5V.Background = couleurHexa;
            btn3V.IsEnabled = false;
            btn5V.IsEnabled = false;

            brdClass.BorderThickness = new Thickness(2);
            brdChall.BorderThickness = new Thickness(0);

            Interfaces.ConfigurationJeu.ModeDeJeu = "Classique";
        }

        private void btn3V_Click(object sender, RoutedEventArgs e)
        {
            nbVictoires = 3;
            brd3V.BorderThickness = new Thickness(2);
            brd5V.BorderThickness = new Thickness(0);
        }

        private void btn5V_Click(object sender, RoutedEventArgs e)
        {
            nbVictoires = 5;
            brd5V.BorderThickness = new Thickness(2);
            brd3V.BorderThickness = new Thickness(0);
        }

        private void btnCroix_Click(object sender, RoutedEventArgs e)
        {
            Service.PageService.PopUp(null);
        }

        private void Btn_LancementPartie(object sender, RoutedEventArgs e)
        {
            // Si mode challenge sans avoir choisi 3V ou 5V -> erreur
            if (modeChallenge && nbVictoires == 0)
            {
                MessageBox.Show("Veuillez choisir 3 ou 5 victoires pour le mode Challenge.", "Erreur");
                return;
            }

            Service.PageService.PopUp(null);
            Service.PageService.Navigate("Game");
        }
    }
}
