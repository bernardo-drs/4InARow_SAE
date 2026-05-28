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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {

        private MainWindow window;
        public Accueil(MainWindow w)
        {
            InitializeComponent();

            window = w;
        }

        private void OnModeButtonClick (object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            new PageService((string)button.Tag);
        }

        private void QuitterButton_Click(object sender, RoutedEventArgs e)
        {
            window.closeFrame.Content = new Quitter(window);
        }

        private void OnMouseEnterButton(object sender, MouseEventArgs e)
        {

            AnimationService.FadeColor(sender, 0.2, "In", null, null);
        }

        private void OnMouseLeaveButton(object sender, MouseEventArgs e)
        {

            AnimationService.FadeColor(sender, 0.2, "Out", null, null);
        }
    }
}
