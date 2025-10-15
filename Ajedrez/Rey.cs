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
            Nombre = 'K';
        }
        public override bool PuedeMover(Tablero tablero, Casillero origen, Casillero destino)
        {
            int dx = Math.Abs(destino.X - origen.X);
            int dy = Math.Abs(destino.Y - origen.Y);
            
            // El rey se mueve una casilla en cualquier direccion
            return (dx <= 1 && dy <= 1) && (dx + dy != 0);
        }
    }
}