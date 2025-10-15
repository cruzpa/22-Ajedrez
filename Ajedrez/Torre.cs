using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Torre : Pieza
    {
        public Torre(ColorPieza color) : base(color)
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wr.png"
            : "img\\br.png";
            Nombre = 'R';
        }
        public override bool PuedeMover(Tablero tablero, Casillero origen, Casillero destino)
        {
            int dx = destino.X - origen.X;
            int dy = destino.Y - origen.Y;

            if (dx != 0 && dy != 0)
                return false; // Solo horizontal o vertical

            int stepX = dx == 0 ? 0 : dx / Math.Abs(dx);
            int stepY = dy == 0 ? 0 : dy / Math.Abs(dy);

            int x = origen.X + stepX, y = origen.Y + stepY;
            while (x != destino.X || y != destino.Y)
            {
                if (tablero.GetCasillero(x, y).Pieza != null)
                    return false;
                x += stepX;
                y += stepY;
            }
            return true;
        }
    }
}