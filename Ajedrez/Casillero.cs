using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Casillero
    {

        public int X { get; set; }
        public int Y { get; set; }
        public Pieza Pieza { get; set; }
        public int Ancho { get; set; }
        public Color ColorFondo { get; set; }
        public bool Seleccionado { get; set; } = false;

        public override bool Equals(object obj)
        {
            return obj is Casillero casillero &&
                   X == casillero.X &&
                   Y == casillero.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}