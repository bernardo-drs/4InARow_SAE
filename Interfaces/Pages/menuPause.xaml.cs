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
    /// Logique d'interaction pour menuPause.xaml
    /// </summary>
    public partial class menuPause : Page
    {
        public menuPause()
        {
            InitializeComponent();
        }

        private void OnMouseEnterButton(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, 0.2, "In", null, null);
        }

        private void OnMouseLeaveButton(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, 0.2, "Out", null, null);
        }

        private void OnModeButtonClick(object sender, RoutedEventArgs e)
        {
            PageService.PopUp(null);
            PageService.Navigate("Game");
        }

        private void OnArreterClick(object sender, RoutedEventArgs e)
        {
            MenuPauseGrid.Visibility = Visibility.Collapsed; 
            ConfirmationGrid.Visibility = Visibility.Visible;
        }

        private void OnAnnulerArretClick(object sender, RoutedEventArgs e)
        {
            ConfirmationGrid.Visibility = Visibility.Collapsed;
            MenuPauseGrid.Visibility = Visibility.Visible;
        }

        private void OnConfirmerArretClick(object sender, RoutedEventArgs e)
        {
            PageService.PopUp(null);
            PageService.Navigate("Accueil");
        }
    }
}
