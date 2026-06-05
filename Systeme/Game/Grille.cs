using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Systeme.Game
{
    public class Grille
    {
        private int nbLignes;
        private int nbColonnes;
        private Cellule[,] cases;

        public Grille(int lignes = 6, int colonnes = 7)
        {
            this.nbLignes = lignes;
            this.nbColonnes = colonnes;
            this.cases = new Cellule[lignes, colonnes];
            ReinitialiserGrille();
        }

        public int GetNBLignes()
        {
            return this.nbLignes;
        }

        public int GetNBColonnes()
        {
            return this.nbColonnes;
        }

        public Cellule[,] GetCases()
        {
            return this.cases;
        }

        public void ReinitialiserGrille()
        {
            for (int l = 0; l < nbLignes; l++)
            {
                for (int c = 0; c < nbColonnes; c++)
                {
                    cases[l, c] = new Cellule(l, c);
                }
            }
        }

        public int GetPremiereLigneLibre(int colonne)
        {
            for (int l = nbLignes - 1; l >= 0; l--)
            {
                if (cases[l, colonne].EstVide())
                {
                    return l;
                }
            }

            return -1;
        }

        public bool PlacerJeton(int colonne, Jeton jeton)
        {
            int ligneLibre = GetPremiereLigneLibre(colonne);
            if (ligneLibre != -1)
            {
                cases[ligneLibre, colonne].RecevoirJeton(jeton);
                return true;
            }

            return false;
        }

        public bool GrilleEstPleine()
        {
            bool ok = true;
            for (int c = 0; c < nbColonnes; c++)
            {
                if (GetPremiereLigneLibre(c) != -1)
                    ok = false;
            }
            return ok;
        }

        public bool VerifierAlignement(string couleur, int nbAAligner)
        {
            for (int l = 0; l < nbLignes; l++)
                for (int c = 0; c <= nbColonnes - nbAAligner; c++)
                {
                    bool ok = true;
                    for (int k = 0; k < nbAAligner; k++)
                        if (cases[l, c + k].GetCouleur() != couleur)
                        {
                            ok = false;
                            break;
                        }

                    if (ok)
                        return true;
                }

            for (int c = 0; c < nbColonnes; c++)
                for (int l = 0; l <= nbLignes - nbAAligner; l++)
                {
                    bool ok = true;
                    for (int k = 0; k < nbAAligner; k++)
                        if (cases[l + k, c].GetCouleur() != couleur)
                        {
                            ok = false;
                            break;
                        }

                    if (ok)
                        return true;
                }

            for (int l = 0; l <= nbLignes - nbAAligner; l++)
                for (int c = 0; c <= nbColonnes - nbAAligner; c++)
                {
                    bool ok = true;
                    for (int k = 0; k < nbAAligner; k++)
                        if (cases[l + k, c + k].GetCouleur() != couleur)
                        {
                            ok = false;
                            break;
                        }

                    if (ok)
                        return true;
                }

            for (int l = nbAAligner - 1; l < nbLignes; l++)
                for (int c = 0; c <= nbColonnes - nbAAligner; c++)
                {
                    bool ok = true;
                    for (int k = 0; k < nbAAligner; k++)
                        if (cases[l - k, c + k].GetCouleur() != couleur)
                        {
                            ok = false;
                            break;
                        }

                    if (ok) return true;
                }

            return false;
        }

    }

}
