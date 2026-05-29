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
        }

        private void btnClassique_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            var couleurHexa = (Brush)bc.ConvertFrom("#605786");

            brd3V.Background = couleurHexa;
            brd5V.Background = couleurHexa;
            btn3V.IsEnabled = false;
            btn5V.IsEnabled = false;
        }
    }
}
