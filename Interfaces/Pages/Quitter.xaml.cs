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
    /// Logique d'interaction pour Quitter.xaml
    /// </summary>
    public partial class Quitter : Page
    {

        MainWindow window;
        public Quitter(MainWindow w)
        {
            InitializeComponent();

            window = w;
        }

        private void NonQuitterButton_Click(object sender, RoutedEventArgs e)
        {
            window.closeFrame.Content = null;
        }

        private void OuiQuitterButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
