using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Interfaces.Service;

namespace Interfaces.Pages
{
    public partial class ParametresIA : Page
    {
        private Button? _selectedDiff = null;
        public Button? SelectedColorIA { get; private set; } = null;

        public string DifficulteSelectionnee => _selectedDiff?.Content?.ToString() ?? "Non sélectionnée";

        public ParametresIA()
        {
            InitializeComponent();
            SelectDiff(BtnFacile);
            this.Loaded += ParametresIA_Loaded;

            this.Loaded += (s, e) => ContrasteService.AppliquerContraste(this);
            this.IsVisibleChanged += (s, e) => ContrasteService.AppliquerContraste(this);
        }

        private void ParametresIA_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in FindVisualChildren<Button>(this))
            {
                if (GetBgColor(btn) == "#ffde59")
                {
                    SelectedColorIA = btn;
                    btn.Tag = "selected";
                    break;
                }
            }
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

        private void DiffButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn) SelectDiff(btn);
        }

        private void SelectDiff(Button btn)
        {
            foreach (var b in new[] { BtnFacile, BtnMoyen, BtnDifficile })
                b.Tag = "unselected";

            btn.Tag = "selected";
            _selectedDiff = btn;
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (SelectedColorIA != null) SelectedColorIA.Tag = "unselected";
            SelectedColorIA = btn; 
            btn.Tag = "selected";
        }

        public static string GetBgColor(Button? btn)
        {
            if (btn?.Background is SolidColorBrush brush)
            {
                Color c = brush.Color;
                return $"#{c.R:X2}{c.G:X2}{c.B:X2}".ToLower();
            }
            return "";
        }
    }
}