using System;
using System.Collections.Generic;
using System.Text;
using Systeme.Game;

namespace Systeme.User
{
    public class IntelligenceArtificielle : Joueur
    {
        private int niveauIA;
        private const int F = 4;

        public IntelligenceArtificielle(int id, string pseudo, string couleurIA, int niveau) : base(id, pseudo, couleurIA)
        {
            this.niveauIA = niveau;
        }

        public int Minimax(int profondeur, bool estMax, int alpha, int beta, Grille jeu, Partie game)
        {
            int meilleurScore;
            int score;
            string couleurHumain = game.GetListeParticipant()[0].GetCouleurJeton();
            string couleurIA = game.GetListeParticipant()[1].GetCouleurJeton();

            if (profondeur == 0 || jeu.GrilleEstPleine())
            {
                return heuristique(jeu, game);
            }

            if (estMax)
            {
                meilleurScore = int.MinValue;

                for (int c = 0; c < jeu.GetNBColonnes(); c++)
                {
                    if (jeu.GetPremiereLigneLibre(c) != -1)
                    {
                        int ligneLibre = jeu.GetPremiereLigneLibre(c);
                        jeu.PlacerJeton(c, new Jeton(couleurIA));

                        if (jeu.VerifierAlignement(couleurIA, F))
                        {
                            jeu.GetCases()[ligneLibre, c].Vider();
                            return 10000 + profondeur;
                        }

                        score = Minimax(profondeur - 1, false, alpha, beta, jeu, game);
                        meilleurScore = Math.Max(meilleurScore, score);

                        jeu.GetCases()[ligneLibre, c].Vider();

                        alpha = Math.Max(alpha, meilleurScore);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
                return meilleurScore;
            }
            else
            {
                meilleurScore = int.MaxValue;

                for (int c = 0; c < jeu.GetNBColonnes(); c++)
                {
                    if (jeu.GetPremiereLigneLibre(c) != -1)
                    {
                        int ligneLibre = jeu.GetPremiereLigneLibre(c);
                        jeu.PlacerJeton(c, new Jeton(couleurHumain));

                        if (jeu.VerifierAlignement(couleurHumain, F))
                        {
                            jeu.GetCases()[ligneLibre, c].Vider();
                            return -10000 - profondeur;
                        }

                        score = Minimax(profondeur - 1, true, alpha, beta, jeu, game);
                        meilleurScore = Math.Min(meilleurScore, score);

                        jeu.GetCases()[ligneLibre, c].Vider();

                        beta = Math.Min(beta, meilleurScore);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
                return meilleurScore;
            }

        }

        public int compter(Cellule[] fenetre, string couleur)
        {
            int i, nb;

            nb = 0;
            for (i = 0; i < F; i++)
            {
                if (!(fenetre[i].EstVide()))
                {
                    if (fenetre[i].GetCouleur() == couleur)
                    {
                        nb++;
                    }
                }
            }
            return nb;
        }

        public bool CaseEstJouable(Grille jeu, int coordX, int coordY)
        {
            if (coordX == jeu.GetNBLignes() - 1)
                return true;
            else
                return !(jeu.GetCases()[coordX + 1, coordY].EstVide());
        }

        public int EvaluerFenetre(Grille jeu, Cellule[] fenetre, string couleurHumain, string couleurIA)
        {
            int i, j, k, m, score, nbVides, nbIA, nbHumain;
            bool[] tabVide = new bool[F];

            for (m = 0; m < F; m++)
            {
                tabVide[m] = false;
            }

            score = 0;
            nbIA = compter(fenetre, couleurIA);
            nbHumain = compter(fenetre, couleurHumain);
            nbVides = 0;

            for (i = 0; i < F; i++)
            {
                if (fenetre[i].EstVide())
                {
                    nbVides++;
                    tabVide[i] = true;
                }
            }

            if (nbIA == 4)
                return 10000;
            else if (nbIA == 3 && nbVides == 1)
            {
                for (j = 0; j < F; j++)
                {
                    if (tabVide[j])
                    {
                        if (CaseEstJouable(jeu, fenetre[j].GetX(), fenetre[j].GetY()))
                            score = score + 100;
                        else
                            score = score + 50;
                    }
                }
            }
            else if (nbIA == 2 && nbVides == 2)
                score = score + 10;
            else if (nbIA == 1)
            {
                score++;
            }

            if (nbHumain == 4)
                return -10000;
            else if (nbHumain == 3 && nbVides == 1)
            {
                for (k = 0; k < F; k++)
                {
                    if (tabVide[k])
                    {
                        if (CaseEstJouable(jeu, fenetre[k].GetX(), fenetre[k].GetY()))
                            score = score - 500;
                        else
                            score = score - 50;
                    }
                }
            }
            else if (nbHumain == 2 && nbVides == 2)
                score = score - 10;
            else if (nbHumain == 1)
            {
                score--;
            }

            return score;
        }

        public int heuristique(Grille jeu, Partie game)
        {
            int i, j, nlignes, ncolonnes, scoreTotal;
            string couleur1, couleur2;
            Cellule[] fenetre;

            scoreTotal = 0;
            couleur1 = game.GetListeParticipant()[0].GetCouleurJeton();
            couleur2 = game.GetListeParticipant()[1].GetCouleurJeton();
            nlignes = jeu.GetNBLignes();
            ncolonnes = jeu.GetNBColonnes();

            for (i = 0; i < nlignes; i++)
            {
                for (j = 0; j <= ncolonnes - F; j++)
                {
                    fenetre = [jeu.GetCases()[i, j], jeu.GetCases()[i, j + 1], jeu.GetCases()[i, j + 2], jeu.GetCases()[i, j + 3]];
                    scoreTotal = scoreTotal + EvaluerFenetre(jeu, fenetre, couleur1, couleur2);
                }
            }

            for (j = 0; j < ncolonnes; j++)
            {
                for (i = 0; i <= nlignes - F; i++)
                {
                    fenetre = [jeu.GetCases()[i, j], jeu.GetCases()[i + 1, j], jeu.GetCases()[i + 2, j], jeu.GetCases()[i + 3, j]];
                    scoreTotal = scoreTotal + EvaluerFenetre(jeu, fenetre, couleur1, couleur2);
                }
            }

            for (i = 0; i <= nlignes - F; i++)
            {
                for (j = 0; j <= ncolonnes - F; j++)
                {
                    fenetre = [jeu.GetCases()[i, j], jeu.GetCases()[i + 1, j + 1], jeu.GetCases()[i + 2, j + 2], jeu.GetCases()[i + 3, j + 3]];
                    scoreTotal = scoreTotal + EvaluerFenetre(jeu, fenetre, couleur1, couleur2);
                }
            }

            for (i = F - 1; i < nlignes; i++)
            {
                for (j = 0; j <= ncolonnes - 4; j++)
                {
                    fenetre = [jeu.GetCases()[i, j], jeu.GetCases()[i - 1, j + 1], jeu.GetCases()[i - 2, j + 2], jeu.GetCases()[i - 3, j + 3]];
                    scoreTotal = scoreTotal + EvaluerFenetre(jeu, fenetre, couleur1, couleur2);
                }
            }

            return scoreTotal;

        }

        public override int ChoisirCoup(Grille plateau, Partie jeu)
        {
            Console.WriteLine($"niveauIA = {niveauIA}");
            // Facile : joue aléatoirement
            if (niveauIA == 1)
            {
                Random rng = new Random(Guid.NewGuid().GetHashCode());
                List<int> colonnesDisponibles = new List<int>();
                for (int c = 0; c < plateau.GetNBColonnes(); c++)
                    if (plateau.GetPremiereLigneLibre(c) != -1)
                        colonnesDisponibles.Add(c);
                return colonnesDisponibles[rng.Next(colonnesDisponibles.Count)];
            }

            int meilleurScore = int.MinValue;
            int meilleurColonne = 0;
            int profondeurMax = niveauIA;

            for (int c = 0; c < plateau.GetNBColonnes(); c++)
            {
                if (plateau.GetPremiereLigneLibre(c) != -1)
                {
                    int ligneLibre = plateau.GetPremiereLigneLibre(c);
                    plateau.PlacerJeton(c, new Jeton(this.GetCouleurJeton()));

                    if (plateau.VerifierAlignement(this.GetCouleurJeton(), F))
                    {
                        plateau.GetCases()[ligneLibre, c].Vider();
                        return c;
                    }

                    int score = Minimax(profondeurMax - 1, false, int.MinValue, int.MaxValue, plateau, jeu);

                    plateau.GetCases()[ligneLibre, c].Vider();

                    if (score > meilleurScore)
                    {
                        meilleurScore = score;
                        meilleurColonne = c;
                    }

                }
            }
            return meilleurColonne;
        }
    }
}
