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
            Joueur j2;
            if (ConfigurationJeu.Joueur2EstBot)
            {
                j2 = new IntelligenceArtificielle(2, "IA", ConfigurationJeu.CouleurJoueur2, ConfigurationJeu.NiveauIA);
            }
            else
                j2 = new Humain(2, ConfigurationJeu.NomJoueur2, ConfigurationJeu.CouleurJoueur2, "", "");
            _partie = new Partie(j1, j2, ConfigurationJeu.JetonsPourGagner, ConfigurationJeu.HauteurGrille, ConfigurationJeu.LargeurGrille);
            _partie.DemarrerPartie();

            _secondesRestantes = ConvertirLimiteTemps(ConfigurationJeu.LimiteTemps) + 1;
            StartTimer();

            if (ConfigurationJeu.ModeDeJeu == "Challenge")
            {
                ScoreJ1.Visibility = Visibility.Visible;
                ScoreJ2.Visibility = Visibility.Visible;

                ScoreJ1.Text = ConfigurationJeu.ScoreJoueur1.ToString();
                ScoreJ2.Text = ConfigurationJeu.ScoreJoueur2.ToString();

            }

            this.Loaded += (s, e) => {Window.GetWindow(this).KeyDown += Game_KeyDown;};

            this.Unloaded += (s, e) => {var win = Window.GetWindow(this);
                if (win != null) win.KeyDown -= Game_KeyDown;};

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

            this.Loaded += (s, e) => ContrasteService.AppliquerContraste(this);
            this.IsVisibleChanged += (s, e) => ContrasteService.AppliquerContraste(this);
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

                    vb.MouseEnter += (s, e) =>
                    {
                        if (s is Viewbox v && v.Tag is int c)
                        {
                            _colonneSelectionnee = c;
                            MettreAJourSurbrillanceColonne();
                        }
                    };

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
            // Pour ne pas pouvoir appuyer lors du tout de l'IA
            if (_partie.GetParticipantActuel() is IntelligenceArtificielle)
                return;

            if (sender is Viewbox vb && vb.Tag is int col)
                JouerDansColonne(col);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            // Si il y a un Popup le clavier deviens inutilisable
            if (PageService.PopUpOuverte) return;
            // Ignore si c'est le tour de l'IA
            if (_partie.GetParticipantActuel() is IntelligenceArtificielle)
                return;

            int col = -1;

            switch (e.Key)
            {
                case Key.Left:
                    // Colonne précédente
                    _colonneSelectionnee = Math.Max(0, _colonneSelectionnee - 1);
                    MettreAJourSurbrillanceColonne();
                    break;
                case Key.Right:
                    // Colonne suivante
                    _colonneSelectionnee = Math.Min(_partie.GetPlateau().GetNBColonnes() - 1, _colonneSelectionnee + 1);
                    MettreAJourSurbrillanceColonne();
                    break;
                case Key.Down:
                case Key.Space:
                    // Jouer dans la colonne sélectionnée
                    JouerDansColonne(_colonneSelectionnee);
                    break;
            }
        }
        private int _colonneSelectionnee = 0;

        private void MettreAJourSurbrillanceColonne()
        {
            foreach (UIElement child in GameGrid.Children)
            {
                if (child is Viewbox vb)
                {
                    int col = Grid.GetColumn(vb);
                    int row = Grid.GetRow(vb);

                    // Surbrillance uniquement la ligne du bas de la colonne sélectionnée
                    if (col == _colonneSelectionnee && row == _partie.GetPlateau().GetPremiereLigneLibre(_colonneSelectionnee))
                    {
                        if (vb.Child is Shape shape && shape.Fill is SolidColorBrush brush)
                        {
                            if (brush.Color == Color.FromRgb(0, 30, 80))
                                shape.Opacity = 0.5;
                        }
                    }
                    else
                    {
                        if (vb.Child is Shape shape)
                            shape.Opacity = 1;
                    }
                }
            }
        }

        private void JouerDansColonne(int col)
        {
            Joueur joueurActuel = _partie.GetParticipantActuel();
            Grille plateau = _partie.GetPlateau();

            int ligneLibre = plateau.GetPremiereLigneLibre(col);
            if (ligneLibre == -1) return;

            Jeton jeton = new Jeton(joueurActuel.GetCouleurJeton(), ConfigurationJeu.FormeJeton);
            plateau.PlacerJeton(col, jeton);

            // Mettre à jour l'UI
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

            // Mettre à jour les jetons
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

            // Vérifier fin de partie
            if (_partie.VerifierFin())
            {
                _timer?.Stop();
                Grille p = _partie.GetPlateau();
                bool estNul = p.GrilleEstPleine() && !p.VerifierAlignement(
                    joueurActuel.GetCouleurJeton(), ConfigurationJeu.JetonsPourGagner);

                if (estNul)
                {
                    PageService.PopUp("Egalite");
                    return;
                }

                // Incrémenter le score 
                if (joueurActuel == _partie.GetListeParticipant()[0])
                {
                    ConfigurationJeu.ScoreJoueur1++;
                    ScoreJ1.Text = ConfigurationJeu.ScoreJoueur1.ToString();

                    if (ConfigurationJeu.ModeDeJeu == "Challenge" &&
                        ConfigurationJeu.ScoreJoueur1 >= ConfigurationJeu.VictoiresRequises)
                    {
                        FinChallenge.NomVainqueur = ConfigurationJeu.NomJoueur1;
                        FinChallenge.CouleurVainqueur = ConfigurationJeu.CouleurJoueur1;
                        FinChallenge.NomPerdant = ConfigurationJeu.NomJoueur2;
                        FinChallenge.CouleurPerdant = ConfigurationJeu.CouleurJoueur2;
                        PageService.PopUp("FinChallenge");
                        return;
                    }
                }
                else
                {
                    ConfigurationJeu.ScoreJoueur2++;
                    ScoreJ2.Text = ConfigurationJeu.ScoreJoueur2.ToString();

                    if (ConfigurationJeu.ModeDeJeu == "Challenge" &&
                        ConfigurationJeu.ScoreJoueur2 >= ConfigurationJeu.VictoiresRequises)
                    {
                        FinChallenge.NomVainqueur = ConfigurationJeu.NomJoueur2;
                        FinChallenge.CouleurVainqueur = ConfigurationJeu.CouleurJoueur2;
                        FinChallenge.NomPerdant = ConfigurationJeu.NomJoueur1;
                        FinChallenge.CouleurPerdant = ConfigurationJeu.CouleurJoueur1;
                        PageService.PopUp("FinChallenge");
                        return;
                    }
                }

                // Afficher victoire et continuer
                Victoire.NomGagnant = joueurActuel.GetNomJoueur();
                Victoire.CouleurGagnant = joueurActuel.GetCouleurJeton();
                PageService.PopUp("Victoire");
                return;
            }

            // Changer de joueur
            _partie.ChangerTour();
            MettreAJourSurbrillance();

            // Réinitialiser le timer pour le joueur suivant
            _secondesRestantes = ConvertirLimiteTemps(ConfigurationJeu.LimiteTemps);
            if (_secondesRestantes > 0)
            {
                _timer?.Stop();
                StartTimer();
            }

            // Si c'est au tour de l'IA, jouer automatiquement
            JouerIA();
        }

        private void JouerIA()
        {
            Joueur joueurActuel = _partie.GetParticipantActuel();

            if (joueurActuel is IntelligenceArtificielle ia)
            {
                // Petit délai pour que l'UI se mette à jour avant le coup de l'IA
                DispatcherTimer timerIA = new DispatcherTimer();
                timerIA.Interval = TimeSpan.FromMilliseconds(500);
                timerIA.Tick += (s, e) =>
                {
                    timerIA.Stop();
                    int colIA = ia.ChoisirCoup(_partie.GetPlateau(), _partie);
                    JouerDansColonne(colIA);
                };
                timerIA.Start();
            }
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
                    PlacerJetonAleatoire();
                }
            };
            _timer.Start();
        }

        private void PlacerJetonAleatoire()
        {
            Grille plateau = _partie.GetPlateau();
            int largeur = plateau.GetNBColonnes();

            // Trouver toutes les colonnes non pleines
            List<int> colonnesDisponibles = new List<int>();
            for (int col = 0; col < largeur; col++)
            {
                if (plateau.GetPremiereLigneLibre(col) != -1)
                    colonnesDisponibles.Add(col);
            }

            if (colonnesDisponibles.Count == 0) return; // Grille pleine

            // Choisir une colonne aléatoire
            Random rng = new Random();
            int colAleatoire = colonnesDisponibles[rng.Next(colonnesDisponibles.Count)];

            JouerDansColonne(colAleatoire);
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
