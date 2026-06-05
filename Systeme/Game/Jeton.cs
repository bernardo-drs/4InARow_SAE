using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using Systeme.User;

namespace Systeme.Game
{
    public class Jeton
    {
        private string couleurJeton;
        private string formeJeton;

        public Jeton(string couleur, string forme = "rond")
        {
            this.couleurJeton = couleur;
            this.formeJeton = forme;
        }

        public string GetCouleurJeton()
        {
            return this.couleurJeton;
        }

        public string GetFormeJeton()
        {
            return this.formeJeton;
        }
    }
}
