using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Interfaces.Service;

namespace Interfaces.Pages
{
    public partial class Parametres : Page
    {
        private Button? _selectedColorJ1 = null;

        public Parametres()
        {
            InitializeComponent();
            RightFrame.Content = new ParametresJoueur2();
            BtnTab2J.Style = (Style)FindResource("TabButtonActive");
        }

        private void BtnTab2J_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.Content = new ParametresJoueur2();
            BtnTab2J.Style = (Style)FindResource("TabButtonActive");
            BtnTabIA.Style = (Style)FindResource("TabButton");
        }

        private void BtnTabIA_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.Content = new ParametresIA();
            BtnTabIA.Style = (Style)FindResource("TabButtonActive");
            BtnTab2J.Style = (Style)FindResource("TabButton");
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (_selectedColorJ1 != null) _selectedColorJ1.Tag = "unselected";
            _selectedColorJ1 = btn;
            btn.Tag = "selected";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;

            // Réinitialiser erreurs J1
            ClearError(ErrNomJ1, IconErrNomJ1);
            ClearError(ErrNatJ1, IconErrNatJ1);
            ClearError(ErrCoulJ1, IconErrCoulJ1);

            string nomJ1 = TxtNomJ1.Text.Trim();
            string natJ1 = TxtNatJ1.Text.Trim();
            string coulJ1 = GetBgColor(_selectedColorJ1);

            if (string.IsNullOrWhiteSpace(nomJ1))
            {
                ShowError(ErrNomJ1, IconErrNomJ1, "Merci de remplir tous les champs");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(natJ1))
            {
                ShowError(ErrNatJ1, IconErrNatJ1, "Merci de remplir tous les champs");
                valid = false;
            }

            if (RightFrame.Content is ParametresJoueur2 pageJ2)
            {
                // Réinitialiser erreurs J2
                ClearError(pageJ2.ErrNomJ2, pageJ2.IconErrNomJ2);
                ClearError(pageJ2.ErrNatJ2, pageJ2.IconErrNatJ2);
                ClearError(pageJ2.ErrCoulJ2, pageJ2.IconErrCoulJ2);

                string nomJ2 = pageJ2.TxtNomJ2.Text.Trim();
                string natJ2 = pageJ2.TxtNatJ2.Text.Trim();
                string coulJ2 = GetBgColor(pageJ2.SelectedColorJ2);

                if (string.IsNullOrWhiteSpace(nomJ2))
                {
                    ShowError(pageJ2.ErrNomJ2, pageJ2.IconErrNomJ2, "Merci de remplir tous les champs");
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(natJ2))
                {
                    ShowError(pageJ2.ErrNatJ2, pageJ2.IconErrNatJ2, "Merci de remplir tous les champs");
                    valid = false;
                }

                // Noms identiques
                if (!string.IsNullOrWhiteSpace(nomJ1) && !string.IsNullOrWhiteSpace(nomJ2)
                    && string.Equals(nomJ1, nomJ2, StringComparison.OrdinalIgnoreCase))
                {
                    ShowError(ErrNomJ1, IconErrNomJ1, "Les noms des deux joueurs doivent être différents");
                    ShowError(pageJ2.ErrNomJ2, pageJ2.IconErrNomJ2, "Les noms des deux joueurs doivent être différents");
                    valid = false;
                }

                // Couleurs identiques
                if (!string.IsNullOrEmpty(coulJ1) && !string.IsNullOrEmpty(coulJ2)
                    && coulJ1 == coulJ2)
                {
                    ShowError(ErrCoulJ1, IconErrCoulJ1, "Les couleurs des jetons doivent être différentes");
                    ShowError(pageJ2.ErrCoulJ2, pageJ2.IconErrCoulJ2, "Les couleurs des jetons doivent être différentes");
                    valid = false;
                }

                if (!valid) return;

                MessageBox.Show(
                    $"Joueur 1 : {nomJ1} — {natJ1} — Jeton : {GetCouleurLabel(_selectedColorJ1)}\n" +
                    $"Joueur 2 : {nomJ2} — {natJ2} — Jeton : {GetCouleurLabel(pageJ2.SelectedColorJ2)}",
                    "Paramètres enregistrés",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                PageService.Navigate("ParametreJeu");
            }
            else if (RightFrame.Content is ParametresIA)
            {
                if (!valid) return;

                MessageBox.Show(
                    $"Joueur 1 : {nomJ1} — {natJ1} — Jeton : {GetCouleurLabel(_selectedColorJ1)}",
                    "Paramètres enregistrés",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                PageService.Navigate("ParametreJeu");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            PageService.Navigate("Accueil");
        }

        private static void ShowError(TextBlock msg, TextBlock icon, string text)
        {
            msg.Text = text;
            msg.Visibility = Visibility.Visible;
            icon.Visibility = Visibility.Visible;
        }

        private static void ClearError(TextBlock msg, TextBlock icon)
        {
            msg.Visibility = Visibility.Collapsed;
            icon.Visibility = Visibility.Collapsed;
        }

        private static string GetBgColor(Button? btn)
            => (btn?.Background as SolidColorBrush)?.Color.ToString() ?? "";

        private static string GetCouleurLabel(Button? btn)
        {
            if (btn == null) return "Non sélectionnée";
            string tag = btn.Tag?.ToString() ?? "";
            int idx = tag.IndexOf('_');
            return idx >= 0 ? tag[(idx + 1)..] : tag;
        }
    }
}