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
using System.ComponentModel;
using Interfaces.Pages;
using Interfaces.Service;

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

            PageHandler.Navigate("ChoixModeJeu");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            closeFrame.Content = new Quitter(this);

            e.Cancel = true;
        }
    }
}