using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme.User
{

    public class Player
    {
        private string _pseudoJoueur;
        private int _nombreJetons;
        public Player(string userName)
        {
            _pseudoJoueur = userName;
        }

        public string PseudoJoueur
        {
            get { return _pseudoJoueur; }
            set { _pseudoJoueur = value; }
        }

        public int NombreJetons
        {
            get { return _nombreJetons; }
            set { this._nombreJetons = value; }
        }
    }
}
