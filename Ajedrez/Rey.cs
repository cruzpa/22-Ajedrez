using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Rey : Pieza
    {
        public Rey(ColorPieza color) : base(color)   
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wk.png"
            : "img\\bk.png";
        }
    }
}