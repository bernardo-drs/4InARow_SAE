using Interfaces.Pages;
using Interfaces.Service;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Systeme.Game;

namespace Interfaces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public PageService PageHandler;

        public MainWindow()
        {
            InitializeComponent();

            PageHandler = new PageService(this);

            Accueil AccueilPage = new Accueil(this);

            mainFrame.Navigate(AccueilPage);

            Grille grille = new Grille(15);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            PageService.PopUp("Quitter");

            e.Cancel = true;
        }
    }
}