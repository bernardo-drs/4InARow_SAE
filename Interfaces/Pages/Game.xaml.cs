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
using Systeme.Game;
using Systeme.User;

namespace Interfaces.Pages
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private DispatcherTimer _timer;
        private int _secondesRestantes;
        private Partie _partie;          
        private int _jetonsJ1;
        private int _jetonsJ2;

        public Game()
        {
            InitializeComponent();

            Humain j1 = new Humain(1, ConfigurationJeu.NomJoueur1, ConfigurationJeu.CouleurJoueur1, "", "");
            Humain j2 = new Humain(2, ConfigurationJeu.NomJoueur2, ConfigurationJeu.CouleurJoueur2, "", "");
            _partie = new Partie(j1, j2, ConfigurationJeu.JetonsPourGagner, ConfigurationJeu.HauteurGrille, ConfigurationJeu.LargeurGrille);

            _secondesRestantes = ConvertirLimiteTemps(ConfigurationJeu.LimiteTemps);
            StartTimer();

            ModeJeuText.Text = ConfigurationJeu.ModeDeJeu;
            CreerGrille();
            InitialiserJetons();

            Player1Forme.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur1));
            Player2Forme.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur2));

            BordureJ1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur1));
            BordureJ2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationJeu.CouleurJoueur2));

            BrdJoueur1.BorderBrush = new SolidColorBrush(Colors.White);
            BrdJoueur2.BorderBrush = new SolidColorBrush(Colors.Transparent);

            Game.OnReprendre += () => { if (_secondesRestantes > 0) _timer?.Start(); };
        }

        private void CreerGrille()
        {
            Grille plateau = _partie.GetPlateau();
            int largeur = plateau.GetNBColonnes();
            int hauteur = plateau.GetNBLignes();

            for (int col = 0; col < largeur; col++)
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int row = 0; row < hauteur; row++)
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int row = 0; row < hauteur; row++)
            {
                for (int col = 0; col < largeur; col++)
                {
                    Viewbox vb = new Viewbox
                    {
                        Stretch = Stretch.Uniform,
                        Margin = new Thickness(4),
                        Tag = col,
                        Cursor = Cursors.Hand
                    };
                    vb.Child = SwitchForme(new SolidColorBrush(Color.FromRgb(0, 30, 80)));
                    vb.MouseLeftButtonDown += OnColonneCliquee;

                    Grid.SetRow(vb, row);
                    Grid.SetColumn(vb, col);
                    GameGrid.Children.Add(vb);
                }
            }
        }

        private void InitialiserJetons()
        {
            // Le nombre de jetons = nombre total de cases / 2
            int totalCases = ConfigurationJeu.LargeurGrille * ConfigurationJeu.HauteurGrille;

            // Correction ici : ajout des parenthèses et du double "=="
            if (totalCases % 2 == 0)
            {
                _jetonsJ1 = totalCases / 2;
                _jetonsJ2 = totalCases / 2;
            }
            else
            {
                _jetonsJ1 = (totalCases + 1) / 2;
                _jetonsJ2 = totalCases / 2;
            }
            JetonsJ1.Text = _jetonsJ1.ToString();
            JetonsJ2.Text = _jetonsJ2.ToString();
        }

        private void OnColonneCliquee(object sender, MouseButtonEventArgs e)
        {
            if (sender is Viewbox vb && vb.Tag is int col)
                JouerDansColonne(col);
        }

        private void JouerDansColonne(int col)
        {
            Joueur joueurActuel = _partie.GetParticipantActuel();
            Grille plateau = _partie.GetPlateau();

            // Trouver la ligne avant de placer (pour mettre à jour l'UI)
            int ligneLibre = plateau.GetPremiereLigneLibre(col);
            if (ligneLibre == -1) return; // Colonne pleine

            // Placer le jeton via la logique métier
            Jeton jeton = new Jeton(joueurActuel.GetCouleurJeton(), ConfigurationJeu.FormeJeton);
            _partie.GetPlateau().PlacerJeton(col, jeton);

            // Mettre à jour la cellule visuelle
            SolidColorBrush couleur = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(joueurActuel.GetCouleurJeton()));

            foreach (UIElement child in GameGrid.Children)
            {
                if (Grid.GetRow(child) == ligneLibre && Grid.GetColumn(child) == col
                    && child is Viewbox vbCible)
                {
                    vbCible.Child = SwitchForme(couleur);
                    break;
                }
            }

            // Mettre à jour les jetons restants
            if (joueurActuel == _partie.GetListeParticipant()[0])
            {
                _jetonsJ1--;
                JetonsJ1.Text = _jetonsJ1.ToString();
            }
            else
            {
                _jetonsJ2--;
                JetonsJ2.Text = _jetonsJ2.ToString();
            }

            // Vérifier la fin de partie AVANT de changer le tour
            if (_partie.VerifierFin())
            {
                _timer?.Stop();
                string gagnant = plateau.GrilleEstPleine() && !plateau.VerifierAlignement(
                    joueurActuel.GetCouleurJeton(), ConfigurationJeu.JetonsPourGagner)
                    ? "Match nul !"
                    : $"{joueurActuel.GetNomJoueur()} a gagné !";

                MessageBox.Show(gagnant, "Fin de partie");
                PageService.Navigate("Accueil");
                return;
            }

            // Changer de joueur
            _partie.ChangerTour();
            MettreAJourSurbrillance();
        }

        private void MettreAJourSurbrillance()
        {
            Joueur actuel = _partie.GetParticipantActuel();

            if (actuel == _partie.GetListeParticipant()[0])
            {
                BrdJoueur1.BorderBrush = new SolidColorBrush(Colors.White);
                BrdJoueur2.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                BrdJoueur1.BorderBrush = new SolidColorBrush(Colors.Transparent);
                BrdJoueur2.BorderBrush = new SolidColorBrush(Colors.White);
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
                "Aucune" => 0  // "Aucune" = le timer reste à 0
            };
        }

        private void StartTimer()
        {
            // Si aucune limite
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

        public static event Action OnReprendre;
        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            _timer?.Stop();
            PageService.PopUp("MenuPause");
        }

        public static void DemanderReprendre()
        {
            OnReprendre?.Invoke();
        }

    }
}
