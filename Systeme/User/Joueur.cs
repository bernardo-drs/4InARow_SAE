using System;
using System.Collections.Generic;
using System.Text;
using Systeme.Game;

namespace Systeme.User
{

    public abstract class Joueur
    {
        private string pseudoJoueur;
        private string couleurJetonJoueur;
        private int nombreVictoireJoueur;
        private int identifiantJoueur;

        public Joueur(int id, string pseudo, string couleur)
        {
            this.identifiantJoueur = id;
            this.pseudoJoueur = pseudo;
            this.couleurJetonJoueur = couleur;
            this.nombreVictoireJoueur = 0;
        }

        public string GetNomJoueur()
        {
            return this.pseudoJoueur;
        }

        public string GetCouleurJeton()
        {
            return this.couleurJetonJoueur;
        }

        public int GetNombreVictoire()
        {
            return this.nombreVictoireJoueur;
        }

        public int GetIdentifiantJoueur()
        {
            return this.identifiantJoueur;
        }

        public void IncrementerVictoire()
        {
            nombreVictoireJoueur++;
        }

        public abstract int ChoisirCoup(Grille plateau, Partie jeu);
    }
}
