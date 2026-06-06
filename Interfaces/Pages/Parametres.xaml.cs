using System;
using System.Windows;
using System.Collections.Generic;
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
            this.Loaded += Parametres_Loaded;
        }

        private void Parametres_Loaded(object sender, RoutedEventArgs e)
        {
            // Sélectionne rouge par défaut pour J1
            foreach (Button btn in FindVisualChildren<Button>(PaletteJ1))
            {
                if (GetBgColor(btn) == "#ff3131")
                {
                    if (_selectedColorJ1 != null) _selectedColorJ1.Tag = "unselected";
                    _selectedColorJ1 = btn;
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

        void BtnSave_Click(object sender, RoutedEventArgs e)
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

            // ══ CAS 1 : Joueur 2 est un humain ══
            if (RightFrame.Content is ParametresJoueur2 pageJ2)
            {
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

                if (!string.IsNullOrWhiteSpace(nomJ1) && !string.IsNullOrWhiteSpace(nomJ2)
                    && string.Equals(nomJ1, nomJ2, StringComparison.OrdinalIgnoreCase))
                {
                    ShowError(ErrNomJ1, IconErrNomJ1, "Les noms des deux joueurs doivent être différents");
                    ShowError(pageJ2.ErrNomJ2, pageJ2.IconErrNomJ2, "Les noms des deux joueurs doivent être différents");
                    valid = false;
                }

                if (!string.IsNullOrEmpty(coulJ1) && !string.IsNullOrEmpty(coulJ2)
                    && coulJ1 == coulJ2)
                {
                    ShowError(ErrCoulJ1, IconErrCoulJ1, "Les couleurs des jetons doivent être différentes");
                    ShowError(pageJ2.ErrCoulJ2, pageJ2.IconErrCoulJ2, "Les couleurs des jetons doivent être différentes");
                    valid = false;
                }

                if (!valid) return;

                // Sauvegarde J1
                ConfigurationJeu.NomJoueur1 = nomJ1;
                ConfigurationJeu.CouleurJoueur1 = coulJ1;

                // Sauvegarde J2 humain
                ConfigurationJeu.NomJoueur2 = nomJ2;
                ConfigurationJeu.CouleurJoueur2 = coulJ2;
                ConfigurationJeu.Joueur2EstBot = false;

                PageService.Navigate("ParametreJeu");
            }

            // ══ CAS 2 : Joueur 2 est un bot ══
            else if (RightFrame.Content is ParametresIA pageIA)
            {
                if (!valid) return;

                string coulIA = GetBgColor(pageIA.SelectedColorIA);

                // Sauvegarde J1
                ConfigurationJeu.NomJoueur1 = nomJ1;
                ConfigurationJeu.CouleurJoueur1 = coulJ1;

                // Sauvegarde bot
                ConfigurationJeu.NomJoueur2 = "Bot";
                ConfigurationJeu.CouleurJoueur2 = string.IsNullOrEmpty(coulIA) ? "#FDD835" : coulIA;
                ConfigurationJeu.Joueur2EstBot = true;

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
        {
            if (btn?.Background is SolidColorBrush brush)
            {
                Color c = brush.Color;
                return $"#{c.R:X2}{c.G:X2}{c.B:X2}".ToLower(); // retourne #ff3131 au lieu de #FFFF3131
            }
            return "";
        }
        private static string GetCouleurLabel(Button? btn)
        {
            if (btn == null) return "Non sélectionnée";
            string tag = btn.Tag?.ToString() ?? "";
            int idx = tag.IndexOf('_');
            return idx >= 0 ? tag[(idx + 1)..] : tag;
        }
    }
}