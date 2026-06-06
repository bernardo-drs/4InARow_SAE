using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Interfaces.Service;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private DispatcherTimer _timer;
        private int _secondesRestantes;
        public Game()
        {
            InitializeComponent();
            _secondesRestantes = ConvertirLimiteTemps(ConfigurationJeu.LimiteTemps);
            StartTimer();
            ModeJeuText.Text = ConfigurationJeu.ModeDeJeu;
            CreerGrille();

            MessageBox.Show(
        $"Joueur 1 : {ConfigurationJeu.NomJoueur1}\n" +
        $"Couleur J1 : {ConfigurationJeu.CouleurJoueur1}\n\n" +
        $"Joueur 2 : {ConfigurationJeu.NomJoueur2}\n" +
        $"Couleur J2 : {ConfigurationJeu.CouleurJoueur2}\n" +
        $"Est un bot : {ConfigurationJeu.Joueur2EstBot}\n\n" +
        $"Grille : {ConfigurationJeu.LargeurGrille} x {ConfigurationJeu.HauteurGrille}\n" +
        $"Jetons pour gagner : {ConfigurationJeu.JetonsPourGagner}\n" +
        $"Limite temps : {ConfigurationJeu.LimiteTemps}\n" +
        $"Forme jeton : {ConfigurationJeu.FormeJeton}",
        "Debug — Vérification des données");
        }

        private void CreerGrille()
        {
            int largeur = ConfigurationJeu.LargeurGrille;
            int hauteur = ConfigurationJeu.HauteurGrille;

            // Créer les colonnes
            for (int col = 0; col < largeur; col++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Créer les lignes
            for (int row = 0; row < hauteur; row++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            // Remplir la grille avec des cellules vides
            for (int row = 0; row < hauteur; row++)
            {
                for (int col = 0; col < largeur; col++)
                {
                    // Viewbox pour centrer et adapter la taille
                    Viewbox vb = new Viewbox
                    {
                        Stretch = Stretch.Uniform,
                        Margin = new Thickness(4)
                    };

                    // Ellipse représentant la cellule vide
                    Ellipse cellule = new Ellipse
                    {
                        Fill = new SolidColorBrush(Color.FromRgb(0, 30, 80)),
                        Width = 50,
                        Height = 50,
                    };

                    vb.Child = SwitchForme(new SolidColorBrush(Color.FromRgb(0, 30, 80)));

                    Grid.SetRow(vb, row);
                    Grid.SetColumn(vb, col);
                    GameGrid.Children.Add(vb);
                }
            }
        }

        private Shape SwitchForme(SolidColorBrush couleur)
        {
            switch (ConfigurationJeu.FormeJeton)
            {
                case "Carré":
                    return new Rectangle
                    {
                        Fill = couleur,
                        Width = 50,
                        Height = 50,
                        RadiusX = 4,
                        RadiusY = 4
                    };

                case "Etoile":
                    return new Path
                    {
                        Fill = couleur,
                        Width = 50,
                        Height = 50,
                        Stretch = Stretch.Uniform,
                        Data = Geometry.Parse("M 50,5 L 61,35 L 95,35 L 68,57 L 79,91 L 50,70 L 21,91 L 32,57 L 5,35 L 39,35 Z")
                    };

                case "Triangle":
                    return new Path
                    {
                        Fill = couleur,
                        Width = 50,
                        Height = 50,
                        Stretch = Stretch.Uniform,
                        Data = Geometry.Parse("M 50,5 L 95,90 L 5,90 Z")
                    };

                default: // "Rond"
                    return new Ellipse
                    {
                        Fill = couleur,
                        Width = 50,
                        Height = 50
                    };
            }
        }

        private int ConvertirLimiteTemps(string limiteTemps)
        {
            return limiteTemps switch
            {
                "10s" => 10,
                "15s" => 15,
                "30s" => 30,
                "1m" => 60,
                "2m" => 120,
                "Aucune" => 0  // "Aucune" ou valeur inconnue = pas de timer
            };
        }

        private void StartTimer()
        {
            // Si aucune limite, on n'affiche pas de timer
            if (_secondesRestantes <= 0)
            {
                TimerText.Text = "0 : 00";
                return;
            }

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                _secondesRestantes--;
                int min = _secondesRestantes / 60;
                int sec = _secondesRestantes % 60;
                TimerText.Text = $"{min} : {sec:D2}";

                if (_secondesRestantes <= 0)
                {
                    _timer.Stop();
                }
            };
            _timer.Start();
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            _timer?.Stop();
            PageService.PopUp("MenuPause");
        }


    }
}
