using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Interfaces.Pages
{
    public partial class ParametresJoueur2 : Page
    {
        public Button? SelectedColorJ2 { get; private set; } = null;

        public ParametresJoueur2()
        {
            InitializeComponent();
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (SelectedColorJ2 != null) SelectedColorJ2.Tag = "unselected";
            SelectedColorJ2 = btn;
            btn.Tag = "selected";
        }
    }
}