using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme.Game
{
    public class Grille
    {
        private Cellule[,] _content;
        private int _size;

        public Grille(int taille)
        {
            this._content = new Cellule[taille, taille];
            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    this._content[x, y] = new Cellule(null, x, y);
                }
            }
        }

        public override string ToString()
        {
            string StringBuffer = "";
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; x < Size; y++)
                {
                    StringBuffer += "X|";
                }
                StringBuffer += "\n";
            }

            return StringBuffer;
        }


        public int Size
        {
            get { return this._size; }
            set { this._size = value; }
        }

        public Cellule[,] Content
        {
            get {  return this._content; }
        }
    }
}
