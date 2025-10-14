using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Caballo : Pieza
    {
        public Caballo(ColorPieza color) : base(color)
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wn.png"
            : "img\\bn.png";
        }
    }
}