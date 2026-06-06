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
            this.Loaded += ParametresJoueur2_Loaded;
        }

        private void ParametresJoueur2_Loaded(object sender, RoutedEventArgs e)
        {
            // Sélectionne jaune par défaut pour J2
            foreach (Button btn in FindVisualChildren<Button>(this))
            {
                if (GetBgColor(btn) == "#ffde59")
                {
                    SelectedColorJ2 = btn;
                    btn.Tag = "selected";
                    break;
                }
            }
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (SelectedColorJ2 != null) SelectedColorJ2.Tag = "unselected";
            SelectedColorJ2 = btn;
            btn.Tag = "selected";
        }

        private static string GetBgColor(Button? btn)
        {
            if (btn?.Background is SolidColorBrush brush)
            {
                Color c = brush.Color;
                return $"#{c.R:X2}{c.G:X2}{c.B:X2}".ToLower(); // retourne #ff3131 au lieu de #FFFF3131
            }
            return "";
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T t) yield return t;
                foreach (var grandChild in FindVisualChildren<T>(child))
                    yield return grandChild;
            }
        }
    }
}