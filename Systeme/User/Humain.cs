using System;
using System.Collections.Generic;
using System.Text;
using Systeme.Game;

namespace Systeme.User
{
    public class Humain : Joueur
    {
        private string nationaliteJoueur;
        private string equipeJoueur;

        public Humain(int id, string pseudo, string couleur, string nationalite, string equipe)
            : base(id, pseudo, couleur)
        {
            this.nationaliteJoueur = nationalite;
            this.equipeJoueur = equipe;
        }

        public string GetNationaliteJoueur() => nationaliteJoueur;

        public override int ChoisirCoup(Grille plateau, Partie jeu)
        {
            return -1;
        }
    }

}
