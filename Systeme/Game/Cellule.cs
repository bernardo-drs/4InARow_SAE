using System;
using System.Collections.Generic;
using System.Text;
using Systeme.Game.Basics;

namespace Systeme.Game
{
    public class Cellule
    {
        private int positionX;
        private int positionY;
        private Jeton Contenu;

        public Cellule(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
            this.Contenu = null;
        }

        public int GetX()
        {
            return this.positionX;
        }

        public int GetY()
        {
            return this.positionY;
        }

        public bool EstOccupee()
        {
            return this.Contenu != null;
        }

        public bool EstVide()
        {
            return this.Contenu == null;
        }

        public void RecevoirJeton(Jeton j)
        {
            Contenu = j;
        }

        public void Vider()
        {
            Contenu = null;
        }

        public string GetCouleur()
        {
            if (EstVide())
                return "Vide";

            return Contenu.GetCouleurJeton();
        }
    }

}
