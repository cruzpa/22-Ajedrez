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

            // El rey se mueve una casilla en cualquier dirección
            if ((dx <= 1 && dy <= 1) && (dx + dy != 0))
            {
                // Simular el movimiento
                Pieza piezaDestinoOriginal = destino.Pieza;
                destino.Pieza = origen.Pieza;
                origen.Pieza = null;

                // Verificar si el rey queda en jaque
                bool enJaque = tablero.ReyEnJaque(this.Color);

                // Revertir el movimiento simulado
                origen.Pieza = destino.Pieza;
                destino.Pieza = piezaDestinoOriginal;

                // Si queda en jaque, no puede moverse
                return !enJaque;
            }
            return false;
        }
    }
}