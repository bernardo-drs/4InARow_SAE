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
using Interfaces.Service;
using Interfaces;

namespace Interfaces.Pages
{
    public partial class ParametreJeu : Page
    {
        bool click = false;

        Border? active1 = null;
        Border? active2 = null;
        Border? active3 = null;
        Border? active4 = null;

        int colonne1;
        int colonne2;
        int colonne3;

        int row1 = -1;
        int row2 = -1;
        int row3 = -1;
        int row4 = -1;

        public ParametreJeu()
        {
            InitializeComponent();

            this.Loaded += (s, e) => ContrasteService.AppliquerContraste(this);
            this.IsVisibleChanged += (s, e) => ContrasteService.AppliquerContraste(this);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, 0.3, "In", null, null);

            if (sender is Border b)
                b.BorderThickness = new Thickness(2);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, 0.3, "Out", null, null);

            if (sender is Border b)
            {
                Grid? g = TrouverParentGrid(b);
                if (g == null) return;

                int colonne = Grid.GetColumn(g);

                if (colonne == 0)
                {
                    if (g.Parent is Grid gp)
                        colonne = Grid.GetColumn(gp);
                    else
                        colonne = Grid.GetColumn(b);
                }

                int row = Grid.GetRow(b);

                if (colonne == 1)
                {
                    if (row != row1)
                        b.BorderThickness = new Thickness(0);
                }
                else if (colonne == 3)
                {
                    int rowB = Grid.GetRow(b);
                    bool estJoueurCommence = (rowB == row4 && b == active4) || rowB == 9;

                    if (estJoueurCommence)
                    {
                        if (b != active4)
                            b.BorderThickness = new Thickness(0);
                    }
                    else
                    {
                        if (rowB != row2)
                            b.BorderThickness = new Thickness(0);
                    }
                }
                else if (colonne == 5)
                {
                    if (row != row3)
                        b.BorderThickness = new Thickness(0);
                }
                else
                {
                    b.BorderThickness = new Thickness(0);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button bouton)) return;

            Border? b = TrouverParentBorder(bouton);
            if (b == null) return;

            Grid? g = TrouverParentGrid(b);
            if (g == null) return;

            int colonne = Grid.GetColumn(g);

            if (colonne == 0)
            {
                if (g.Parent is Grid gp)
                    colonne = Grid.GetColumn(gp);
                else
                    colonne = Grid.GetColumn(b);
            }

            if (colonne == 1)
            {
                if (active1 != null)
                    active1.BorderThickness = new Thickness(0);

                b.BorderThickness = new Thickness(2);
                colonne1 = colonne;
                row1 = Grid.GetRow(b);
                active1 = b;
                click = true;
            }

            if (colonne == 3)
            {
                int rowB = Grid.GetRow(b);
                bool estJoueurCommence = bouton.Tag?.ToString() == "JoueurCommence";

                if (estJoueurCommence)
                {
                    if (active4 != null)
                        active4.BorderThickness = new Thickness(0);

                    b.BorderThickness = new Thickness(2);
                    row4 = rowB;
                    active4 = b;
                    click = true;
                }
                else
                {
                    if (active2 != null)
                        active2.BorderThickness = new Thickness(0);

                    b.BorderThickness = new Thickness(2);
                    colonne2 = colonne;
                    row2 = rowB;
                    active2 = b;
                    click = true;
                }
            }

            if (colonne == 5)
            {
                if (active3 != null)
                    active3.BorderThickness = new Thickness(0);

                b.BorderThickness = new Thickness(2);
                colonne3 = colonne;
                row3 = Grid.GetRow(b);
                active3 = b;
                click = true;
            }
        }

        private void btnApplique_Click(object sender, RoutedEventArgs e)
        {
            if (row1 == -1 || row2 == -1 || row3 == -1 || row4 == -1)
            {
                MessageBox.Show("Veuillez sélectionner une option dans chaque catégorie (Taille, Jetons, Temps et Joueur qui commence) avant de commencer !", "Paramètres incomplets");
                return;
            }

            int largeur = 7;
            int hauteur = 6;
            if (row1 == 0) { largeur = 4; hauteur = 4; }
            else if (row1 == 2) { largeur = 5; hauteur = 5; }
            else if (row1 == 4) { largeur = 7; hauteur = 6; }
            else if (row1 == 6) { largeur = 7; hauteur = 8; }
            else if (row1 == 8) { largeur = 8; hauteur = 8; }
            else if (row1 == 10) { largeur = 10; hauteur = 10; }

            int jetons = 4;
            if (row2 == 3) jetons = 3;
            else if (row2 == 5) jetons = 4;
            else if (row2 == 7) jetons = 5;

            if (jetons > largeur && jetons > hauteur)
            {
                MessageBox.Show("La condition de victoire doit être inférieure à la taille de la grille", "Erreur de taille");
                return;
            }

            string temps = "Aucune";
            if (row3 == 0) temps = "Aucune";
            else if (row3 == 2) temps = "10s";
            else if (row3 == 4) temps = "15s";
            else if (row3 == 6) temps = "30s";
            else if (row3 == 8) temps = "1m";
            else if (row3 == 10) temps = "2m";

            if (active4 != null)
                ConfigurationJeu.JoueurQuiCommence = (Grid.GetColumn(active4) == 0) ? 1 : 2;

            ConfigurationJeu.LargeurGrille = largeur;
            ConfigurationJeu.HauteurGrille = hauteur;
            ConfigurationJeu.JetonsPourGagner = jetons;
            ConfigurationJeu.LimiteTemps = temps;

            PageService.PopUp("ChoixModeJeu");
        }

        private Grid? TrouverParentGrid(DependencyObject enfant)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(enfant) ?? (enfant is FrameworkElement fe ? fe.Parent : null);
            while (parent != null && !(parent is Grid))
                parent = VisualTreeHelper.GetParent(parent) ?? (parent is FrameworkElement pfe ? pfe.Parent : null);
            return parent as Grid;
        }

        private Border? TrouverParentBorder(DependencyObject enfant)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(enfant) ?? (enfant is FrameworkElement fe ? fe.Parent : null);
            while (parent != null && !(parent is Border))
                parent = VisualTreeHelper.GetParent(parent) ?? (parent is FrameworkElement pfe ? pfe.Parent : null);
            return parent as Border;
        }

        private void Button_retour(object sender, RoutedEventArgs e)
        {
            PageService.Navigate("Parametres");
        }
    }
}