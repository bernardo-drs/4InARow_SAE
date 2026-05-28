using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme.User
{
    internal class Human : Player
    {
        private string _nationaliteJoueur;

        public Human (string nomJoueur, string nationaliteJoueur) : base(nomJoueur)
        {
            NationaliteJoueur = nationaliteJoueur;
        }

        public string NationaliteJoueur
        {
            get {  return this._nationaliteJoueur; }
            set {  this._nationaliteJoueur = value;}
        }

    }
}
