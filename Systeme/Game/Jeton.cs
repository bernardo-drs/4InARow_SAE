using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using Systeme.User;

namespace Systeme.Game
{
    public class Jeton
    {

        private Player? _owner;
        private string _couleurJeton;

        public Jeton(Player owner, string couleurJeton)
        {
            Owner = owner;
            CouleurJeton = couleurJeton;
        }

        public Player? Owner
        {
            get { return this._owner; }
            set { this._owner = value;}
        }

        public string CouleurJeton
        {
            get { return this._couleurJeton; }
            set {this._couleurJeton = value;}
        }
    }
}
