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
    /// Logique d'interaction pour LeaderBard.xaml
    /// </summary>
    public partial class LeaderBoard: Page
    {

        public LeaderBoard()
        {
            InitializeComponent();

        }

        private void OnLeavePage(object sender, RoutedEventArgs e)
        {
            PageService.Navigate("Accueil");
        }
    }
}