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


    }
}