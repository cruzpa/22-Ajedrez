using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Juego
    {

        public Tablero tablero;

        public Jugador Blancas;
        public Jugador Negras;

        public Turno turno;

        public Casillero casilleroPrevio = null;

        public Juego(Tablero tablero)
        {
            this.tablero = tablero;
        }
        public Juego(Jugador blancas, Jugador negras)
        {
            this.Blancas = blancas;
            this.Negras = negras;
            this.tablero = new Tablero();
            this.turno = Turno.Blancas;
        }

        public void CompararCasillero(Casillero casillero)
        {
            //validar turno
            if (casilleroPrevio == null && casillero.Pieza != null && !MovimientoEsTurnoValido(casillero))
            {
                return;
            }

            //deselecciono
            if (casilleroPrevio != null && casilleroPrevio.Equals(casillero))
            {
                casilleroPrevio = null;
                return;
            }

            // Validar si el rey está en jaque y solo permitir seleccionar el rey si es así
            bool reyEnJaque = tablero.ReyEnJaque(turno == Turno.Blancas ? ColorPieza.Blanco : ColorPieza.Negro);
            if (reyEnJaque && !(casilleroPrevio?.Pieza is Rey))
            {
                // Solo permitir seleccionar el rey del color correspondiente
                if (!(casillero.Pieza is Rey) || casillero.Pieza.Color != (turno == Turno.Blancas ? ColorPieza.Blanco : ColorPieza.Negro))
                {
                    Console.WriteLine("Solo puedes seleccionar el rey porque estás en jaque.");
                    return;
                }
            }

            //selecciono pieza. No comparo pq no tengo contra quien.
            if (casilleroPrevio == null & casillero.Pieza != null)
            {
                Console.WriteLine($"seleccionaste: {casillero}");
                casillero.Seleccionado = true;
                casilleroPrevio = casillero;
                return;
            }

            //muevo pieza validar 
            if (casilleroPrevio != null)
            {
                if (esMovimientoValido(tablero, casillero, casilleroPrevio))
                {
                    casillero.Pieza = casilleroPrevio.Pieza;
                    casilleroPrevio.Pieza = null;
                    casilleroPrevio.Seleccionado = false;

                    RemoverPiezaPorEnPassantSiCorresponde(casillero);
                    DesactivarEnPassant();
                    ActivarEnPassantSiCorresponde(casillero);
                    CoronacionSiCorresponde(casillero);

                    casilleroPrevio = null;
                    Console.WriteLine($"destino: {casillero}");
                    
                    //si movimiento valido -> pasar turno
                    turno = (turno == Turno.Blancas) ? Turno.Negras : Turno.Blancas;
                }
                else
                {
                    Console.WriteLine($"movimiento invalido: {casilleroPrevio} a {casillero}");
                }

            }

        }

        private void RemoverPiezaPorEnPassantSiCorresponde(Casillero casillero)
        {
            if (casillero.Pieza is Peon movedPeon)
            {
                int dx = casillero.X - casilleroPrevio.X;
                int dy = casillero.Y - casilleroPrevio.Y;
                int dir = (movedPeon.Color == ColorPieza.Blanco) ? 1 : -1;

                // movimiento diagonal de captura (dx == ±1) y desplazamiento vertical correcto (dy == dir)
                if (Math.Abs(dx) == 1 && dy == dir)
                {
                    // casillero donde quedaria el peon capturado por en passant
                    var casilleroCapturado = tablero.GetCasillero(casillero.X, casilleroPrevio.Y);
                    if (casilleroCapturado.Pieza is Peon rival && rival.Color != movedPeon.Color && rival.RecienMovidoDoble)
                    {
                        // eliminar el peon rival capturado por en passant
                        casilleroCapturado.Pieza = null;
                        Console.WriteLine($"En Passant: eliminado peón rival en {casilleroCapturado}");
                    }
                }
            }
        }

        private static void CoronacionSiCorresponde(Casillero casillero)
        {
            // Coronacion: si la pieza movida es un peon y llego a la ultima fila del rival, se promociona a Reyna por defecto.
            if (casillero.Pieza is Peon peon)
            {
                bool esFilaCoronacion = (peon.Color == ColorPieza.Blanco && casillero.Y == 8)
                                       || (peon.Color == ColorPieza.Negro && casillero.Y == 1);
                if (esFilaCoronacion)
                {
                    Console.WriteLine($"Coronación: Peón en {casillero} se promueve a Reyna.");
                    casillero.Pieza = new Reyna(peon.Color);
                }
            }
        }

        private void ActivarEnPassantSiCorresponde(Casillero casillero)
        {
            Console.WriteLine("Verificando En Passant...");
            if (casillero.Pieza is Peon peon)
            {
                peon.RecienMovidoDoble = Math.Abs(casillero.Y - casilleroPrevio.Y) == 2;
                Console.WriteLine($"En Passant de casillero {casillero}: {peon.RecienMovidoDoble}");

            }
        }

        private void DesactivarEnPassant()
        {

            Console.WriteLine("Desactivando En Passant...");
            for (int x = 1; x < 9; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    var pieza = tablero.GetCasillero(x, y).Pieza;
                    if (pieza is Peon peon)
                        peon.RecienMovidoDoble = false;
                }
            }
        }

        private bool esMovimientoValido(Tablero tablero, Casillero destino, Casillero origen)
        {
            // No permitir mover desde una casilla vacía
            // Validación de turno: sólo se puede mover una pieza del color correspondiente al turno actual.
            // No permitir mover a una casilla ocupada por una pieza del mismo color
            // delegar a la pieza la validacion de movimiento.
            return MovimientoHayPiezaEnElorigen(origen) 
                //&& MovimientoEsTurnoValido (origen) 
                && !MovimientoCasillaOcupadaPorMismoColor(origen, destino)
                && origen.Pieza.PuedeMover(tablero, origen, destino);
        }

        private bool MovimientoHayPiezaEnElorigen(Casillero origen)
        {
            if (origen.Pieza == null)
            {
                Console.WriteLine("Movimiento invalido: No hay pieza en el casillero origen.");
                return false;
            }
            return true;
        }

        private bool MovimientoEsTurnoValido(Casillero origen)
        {
            if (turno == Turno.Blancas && origen.Pieza.Color != ColorPieza.Blanco)
            {
                Console.WriteLine($"Movimiento invalido: No es turno de las {Turno.Blancas}.");
                return false;
            }
            if (turno == Turno.Negras && origen.Pieza.Color != ColorPieza.Negro)
            {
                Console.WriteLine($"Movimiento invalido: No es turno de las {Turno.Negras}.");
                return false;
            }
            return true;
        }

        private bool MovimientoCasillaOcupadaPorMismoColor(Casillero origen, Casillero destino)
        {
            if (destino.Pieza != null && destino.Pieza.Color == origen.Pieza.Color)
            {
                Console.WriteLine($"Movimiento invalido: Hay una pieza tuya en ese casillero.");
                return true;
            }
            return false;
        }

    }
}