using System;
using System.Collections.Generic;
using System.Text;
using Systeme.Game.Basics;

namespace Systeme.Game
{
    public class Cellule
    {
        private Position _position;
        private Jeton? _jeton;

        public Cellule(Jeton? jeton, int posX, int posY)
        {
            this._position = new Position(posX, posY);

            Jeton = jeton;
        }

        public override string ToString()
        {
            return $"{(Jeton == null ? " " : "X")}|";
        }

        public Jeton? Jeton
        {
            get { return this._jeton; }
            set { this._jeton = value; }
        }

        public Position Position
        {
            get { return this._position; }
        }
    }
}
