using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Systeme.Game
{
    public class Grille
    {
        private Cellule[,] _content;
        private int _size;

        public Grille(int taille)
        {
            Debug.WriteLine("Initialisation de la grille.");

            Size = taille;

            this._content = new Cellule[taille, taille];
            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    this._content[x, y] = new Cellule(null, x, y);
                }
            }
        }
        public int Size
        {
            get { return this._size; }
            set { this._size = value; }
        }

        public Cellule[,] Content
        {
            get { return this._content; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Numéros des colonnes
            sb.Append("   ");
            for (int x = 0; x < Size; x++)
            {
                sb.Append($"{x} ");
            }
            sb.AppendLine();

            // Ligne séparatrice
            sb.Append("  ");
            for (int x = 0; x < Size; x++)
            {
                sb.Append("--");
            }
            sb.AppendLine();

            // Contenu de la grille
            for (int y = 0; y < Size; y++)
            {
                sb.Append($"{y}| ");

                for (int x = 0; x < Size; x++)
                {
                    sb.Append($"{Content[x,y]}");
                }

                sb.AppendLine("");
            }

            return sb.ToString();
        }

    }
}
