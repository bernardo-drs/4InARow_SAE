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
        public ChoixModeJeu()
        {
            InitializeComponent();
        }

        private void btnChallenge_Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            var couleurHexa = (Brush)bc.ConvertFrom("#20114A");

            brd3V.Background = couleurHexa;
            brd5V.Background = couleurHexa;
            btn3V.IsEnabled = true;
            btn5V.IsEnabled = true;

            brdChall.BorderThickness = new Thickness(2);
            brdClass.BorderThickness = new Thickness(0);


        }

        private void btnClassique_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            var couleurHexa = (Brush)bc.ConvertFrom("#605786");

            brd3V.Background = couleurHexa;
            brd5V.Background = couleurHexa;
            btn3V.IsEnabled = false;
            btn5V.IsEnabled = false;

            brdClass.BorderThickness = new Thickness(2);
            brdChall.BorderThickness = new Thickness(0);
        }

        private void btn3V_Click(object sender, RoutedEventArgs e)
        {
            brd3V.BorderThickness = new Thickness(2);
            brd5V.BorderThickness = new Thickness(0);
        }

        private void btn5V_Click(object sender, RoutedEventArgs e)
        {
            brd5V.BorderThickness = new Thickness(2);
            brd3V.BorderThickness = new Thickness(0);
        }

        private void btnCroix_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(null);
        }
    }
}
