using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Peon : Pieza
    {
        public bool RecienMovidoDoble { get; set; }
        public Peon(ColorPieza color) : base(color)
        {
            Imagen = color == ColorPieza.Blanco
            ? "img\\wp.png"
            : "img\\bp.png";
            Nombre = 'P';
            RecienMovidoDoble = false;
        }
        public override bool PuedeMover(Tablero tablero, Casillero origen, Casillero destino)
        {
            int dir = (Color == ColorPieza.Blanco) ? 1 : -1;
            int dx = destino.X - origen.X;
            int dy = destino.Y - origen.Y;

            // Movimiento hacia adelante
            if (dx == 0)
            {
                // Una casilla adelante
                if (dy == dir && destino.Pieza == null)
                    return true;
                // Dos casillas desde la posición inicial
                if (dy == 2 * dir && destino.Pieza == null && origen.Y == ((Color == ColorPieza.Blanco) ? 2 : 7))
                {
                    // Verificar que la casilla intermedia esté vacía
                    int yIntermedia = origen.Y + dir;
                    if (tablero.GetCasillero(origen.X, yIntermedia).Pieza == null)
                        return true;
                }
            }
            // Captura en diagonal
            if (Math.Abs(dx) == 1 && dy == dir && destino.Pieza != null && destino.Pieza.Color != Color)
                return true;

            // En passant
            Casillero adyacente = tablero.GetCasillero(destino.X, origen.Y);
            if (adyacente.Pieza is Peon peonRival
                && peonRival.Color != this.Color
                && peonRival.RecienMovidoDoble
                && destino.Pieza == null)
            {
                return true;
            }
            //todo: validar coronacion.

            return false;
        }
    }

}