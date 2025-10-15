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
            Nombre = 'N';
        }
        public override bool PuedeMover(Tablero tablero, Casillero origen, Casillero destino)
        {
            int dx = Math.Abs(destino.X - origen.X);
            int dy = Math.Abs(destino.Y - origen.Y);

            // Movimiento en L
            return (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
        }
    }
}