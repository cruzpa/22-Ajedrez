using System;

namespace BE
{
    public class Alfil : Pieza
    {
        public Alfil(ColorPieza color) : base(color)
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wb.png"
            : "img\\bb.png";
            Nombre = 'B';
        }
        public override bool PuedeMover(Tablero tablero, Casillero origen, Casillero destino)
        {
            int dx = destino.X - origen.X;
            int dy = destino.Y - origen.Y;

            if (Math.Abs(dx) != Math.Abs(dy))
                return false; // Solo diagonal
            if(dx == 0 || dy == 0)
            {
                return false; //no se puede mover a la misma fila o misma columna
            }

            int stepX = dx / Math.Abs(dx);
            int stepY = dy / Math.Abs(dy);

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