using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Reyna : Pieza
    {
        public Reyna(ColorPieza color) : base(color)
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wq.png"
            : "img\\bq.png";
        }
    }
}