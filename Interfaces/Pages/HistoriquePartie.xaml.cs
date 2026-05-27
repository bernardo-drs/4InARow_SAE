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
    /// Logique d'interaction pour HistoriquePartie.xaml
    /// </summary>
    public partial class HistoriquePartie : Page
    {

        private MainWindow Window;

        public HistoriquePartie(MainWindow w)
        {
            InitializeComponent();

            Window = w;
        }

        private void OnLeavePage(object sender, RoutedEventArgs e)
        {
            Window.mainFrame.Content = new Accueil();
        }
    }
}
