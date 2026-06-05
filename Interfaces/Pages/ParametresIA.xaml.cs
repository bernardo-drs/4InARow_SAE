using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interfaces.Pages
{
    public partial class ParametresIA : Page
    {
        private Button? _selectedDiff = null;
        private Button? _selectedColor = null;

        public string DifficulteSelectionnee => _selectedDiff?.Content?.ToString() ?? "Non sélectionnée";
        public string CouleurSelectionnee => GetLabel(_selectedColor);

        public ParametresIA()
        {
            InitializeComponent();
            SelectDiff(BtnFacile);
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
            if (_selectedColor != null) _selectedColor.Tag = "unselected";
            _selectedColor = btn;
            btn.Tag = "selected";
        }

        private static string GetLabel(Button? b)
        {
            if (b == null) return "Non sélectionnée";
            string t = b.Tag?.ToString() ?? "";
            int i = t.IndexOf('_');
            return i >= 0 ? t[(i + 1)..] : t;
        }
    }
}