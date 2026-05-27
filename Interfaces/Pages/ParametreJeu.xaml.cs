using Interfaces.Service;
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
    /// Logique d'interaction pour ParametreJeu.xaml
    /// </summary>
    public partial class ParametreJeu : Page
    {
        bool click = false;

        Border? active1 = null;
        Border? active2 = null;
        Border? active3 = null;

        int colonne1;
        int colonne2;
        int colonne3;

        int row1 = 10000;
        int row2 = 10000;
        int row3 = 10000;


        public ParametreJeu()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, "In");
            Border b = (Border)sender;

            b.BorderThickness = new Thickness(2);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationService.FadeColor(sender, "Out");
            Border b = (Border)sender;
            Grid g = (Grid)b.Parent;
            Grid gp;

            int colonne = Grid.GetColumn(g);

            if (colonne == 0)
            {
                if (g.Parent is Grid)
                {
                    gp = (Grid)g.Parent;
                    colonne = Grid.GetColumn(gp);
                }
                else
                {
                    colonne = Grid.GetColumn(b);
                }
            }

            int row = Grid.GetRow(b);

            if (colonne == 1)
            {
                if (row != row1)
                    b.BorderThickness = new Thickness(0);
            }
            else if (colonne == 3)
            {
                if (row != row2)
                    b.BorderThickness = new Thickness(0);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            Viewbox vb = (Viewbox)bouton.Parent;
            Border b = (Border)vb.Parent;
            Grid g = (Grid)b.Parent;
            Grid gp;

            int colonne = Grid.GetColumn(g);

            if (colonne == 0)
            {
                if (g.Parent is Grid)
                {
                    gp = (Grid)g.Parent;
                    colonne = Grid.GetColumn(gp);
                }
                else
                {
                    colonne = Grid.GetColumn(b);
                }
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
                if (active2 != null)
                    active2.BorderThickness = new Thickness(0);

                b.BorderThickness = new Thickness(2);

                colonne2 = colonne;
                row2 = Grid.GetRow(b);
                active2 = b;

                click = true;

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
            bool estGrilleTailleQuatre = (colonne1 == 1 && row1 == 0);
            bool estCinqJetonsAlignes = (colonne2 == 3 && row2 == 7);

            if (estGrilleTailleQuatre && estCinqJetonsAlignes)
            {
                string message = "La condition de victoire doit être inférieure à la taille de la grille";
                MessageBox.Show(message, "Erreur de taille");
            }
        }
    }
}
