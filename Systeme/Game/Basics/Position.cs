using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme.Game.Basics
{
    public class Position
    {

        private int _x;
        private int _y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public int X
        {
            get { return this._x; }
            set { this._x = value; }
        }

        public int Y
        {
            get { return this._y; }
            set { this._y = value; }

        }
    }
}
