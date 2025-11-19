using System;

namespace BE
{
    public class Reyna : Pieza
    {
        public Reyna(ColorPieza color) : base(color)
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wq.png"
            : "img\\bq.png";

            Nombre = 'Q';
        }
        public override bool PuedeMover(Tablero tablero, Casillero origen, Casillero destino)
        {
            int dx = Math.Abs(destino.X - origen.X);
            int dy = Math.Abs(destino.Y - origen.Y);

            // Movimiento en línea recta o diagonal
            if (dx == dy || dx == 0 || dy == 0)
            {
                // Verificar que el camino esté libre
                int stepX = dx == 0 ? 0 : (destino.X - origen.X) / dx;
                int stepY = dy == 0 ? 0 : (destino.Y - origen.Y) / dy;
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
            return false;
        }
    }
}