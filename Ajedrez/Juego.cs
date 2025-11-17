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
        
        public event Action<ColorPieza, bool> FinPartida; // Color del ganador, bool esEmpate

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
            
            // Verificar estado del rey del jugador actual
            ColorPieza colorTurnoActual = (turno == Turno.Blancas) ? ColorPieza.Blanco : ColorPieza.Negro;

            if (tablero.ReyEnJaque(colorTurnoActual))
            {
                Console.WriteLine($" El rey {turno} esta en JAQUE");
            }

            //validar turno
            if (casilleroPrevio == null && casillero.Pieza != null && !MovimientoEsTurnoValido(casillero))
            {
                Console.WriteLine("No es el turno de esta pieza");
                return;
            }

            //deselecciono
            if (casilleroPrevio != null && casilleroPrevio.Equals(casillero))
            {
                casilleroPrevio.Seleccionado = false;
                casilleroPrevio = null;
                return;
            }

            //selecciono pieza. No comparo pq no tengo contra quien.
            if (casilleroPrevio == null && casillero.Pieza != null)
            {
                Console.WriteLine($"seleccionaste: {casillero}");
                casillero.Seleccionado = true;
                casilleroPrevio = casillero;
                return;
            }

            // click en casillero vacio sin seleccion previa
            if (casilleroPrevio == null && casillero.Pieza == null)
            {
                return;
            }

            //muevo pieza validar 
            if (casilleroPrevio != null)
            {
                Console.WriteLine($"Intentando mover: {casilleroPrevio.Pieza.Nombre} de {casilleroPrevio} a {casillero}");
                
                if (esMovimientoValido(tablero, casillero, casilleroPrevio))
                {
                    // Guardar información antes de mover
                    Pieza piezaMovida = casilleroPrevio.Pieza;
                    Pieza piezaCapturada = casillero.Pieza;
                    string origenStr = casilleroPrevio.ToString();
                    string destinoStr = casillero.ToString();
                    string infoCaptura = piezaCapturada != null ? $" (Capturando {piezaCapturada.Nombre})" : "";
                    
                    Console.WriteLine($"Movimiento valido: {infoCaptura}");
                    
                    //mover pieza
                    casillero.Pieza = casilleroPrevio.Pieza;

                    //remover pieza por en passant (no puedo hacerlo como removia ya que no es ni origen ni destino)
                    RemoverPiezaPorEnPassantSiCorresponde(casillero);
                    DesactivarEnPassant();
                    ActivarEnPassantSiCorresponde(casillero);
                    CoronacionSiCorresponde(casillero);

                    //limpio pieza previa
                    casilleroPrevio.Pieza = null;
                    casilleroPrevio.Seleccionado = false;
                    casilleroPrevio = null;

                    Console.WriteLine($"Movimiento completado: {piezaMovida.Nombre} de {origenStr} a {destinoStr}");
                    //todo: aca escribir lista de movimientos.

                    //si movimiento valido -> pasar turno
                    turno = (turno == Turno.Blancas) ? Turno.Negras : Turno.Blancas;
                    Console.WriteLine($"Ahora es turno de: {turno}");
                    
                    // Verificar condiciones de victoria después del movimiento
                    VerificarCondicionesDeVictoria();
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
                    Console.WriteLine($"Coronacion: Peon en {casillero} se promueve a Reyna.");
                    casillero.Pieza = new Reyna(peon.Color);
                }
            }
        }

        private void ActivarEnPassantSiCorresponde(Casillero casillero)
        {
            if (casillero.Pieza is Peon peon)
            {
                bool movioDoble = Math.Abs(casillero.Y - casilleroPrevio.Y) == 2;
                peon.RecienMovidoDoble = movioDoble;
                if (movioDoble)
                {
                    Console.WriteLine($"En Passant activado: Peon {peon.Color} movio dos casillas desde {casilleroPrevio} a {casillero}");
                }
            }
        }

        private void DesactivarEnPassant()
        {
            for (int x = 1; x < 9; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    var pieza = tablero.GetCasillero(x, y).Pieza;
                    if (pieza is Peon peon && peon.RecienMovidoDoble)
                    {
                        peon.RecienMovidoDoble = false;
                    }
                }
            }
        }

        private bool esMovimientoValido(Tablero tablero, Casillero destino, Casillero origen)
        {
            // No permitir mover desde una casilla vacía
            // Validación de turno: sólo se puede mover una pieza del color correspondiente al turno actual.
            // No permitir mover a una casilla ocupada por una pieza del mismo color
            // delegar a la pieza la validacion de movimiento.
            if (!MovimientoHayPiezaEnElorigen(origen))
            {
                return false;
            }
            
            if (MovimientoCasillaOcupadaPorMismoColor(origen, destino))
            {
                return false;
            }
            
            // Validar reglas de movimiento de la pieza
            if (!origen.Pieza.PuedeMover(tablero, origen, destino))
            {
                return false;
            }

            // Verificar que el movimiento no deje al rey del jugador en jaque
            // (excepto para el rey, que ya lo verifica en su metodo PuedeMover)
            if (!(origen.Pieza is Rey))
            {
                //Console.WriteLine($"Verificando si el movimiento deja al rey en jaque");
                
                // Simular el movimiento
                Pieza piezaOrigen = origen.Pieza;
                Pieza piezaDestino = destino.Pieza;
                ColorPieza colorJugador = piezaOrigen.Color;

                // Realizar el movimiento temporal
                destino.Pieza = piezaOrigen;
                origen.Pieza = null;

                // Verificar si el rey queda en jaque después del movimiento
                bool reyEnJaque = tablero.ReyEnJaque(colorJugador);

                // Revertir el movimiento
                origen.Pieza = piezaOrigen;
                destino.Pieza = piezaDestino;

                // El movimiento no es válido si deja al rey en jaque
                if (reyEnJaque)
                {
                    return false;
                }
            }

            //movimiento valido
            return true;
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

        // Verifica si un movimiento es legal (no deja al rey del jugador en jaque)
        private bool EsMovimientoLegal(Casillero origen, Casillero destino)
        {
            if (!esMovimientoValido(tablero, destino, origen))
                return false;

            // simular el movimiento
            Pieza piezaOrigen = origen.Pieza;
            Pieza piezaDestino = destino.Pieza;
            ColorPieza colorJugador = piezaOrigen.Color;

            // realizar el movimiento temporal
            destino.Pieza = piezaOrigen;
            origen.Pieza = null;

            // verificar si el rey queda en jaque despues del movimiento
            bool reyEnJaque = tablero.ReyEnJaque(colorJugador);

            // revertir el movimiento
            origen.Pieza = piezaOrigen;
            destino.Pieza = piezaDestino;

            // el movimiento es legal si no deja al rey en jaque
            return !reyEnJaque;
        }

        
        // Verifica si el jugador del turno actual tiene movimientos legales disponibles
        private bool TieneMovimientosLegales(ColorPieza color)
        {
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    var origen = tablero.GetCasillero(x, y);
                    if (origen.Pieza == null || origen.Pieza.Color != color)
                        continue;

                    // probar todos los posibles destinos
                    for (int dx = 1; dx <= 8; dx++)
                    {
                        for (int dy = 1; dy <= 8; dy++)
                        {
                            var destino = tablero.GetCasillero(dx, dy);
                            if (EsMovimientoLegal(origen, destino))
                            {
                                return true; // tiene al menos un movimiento legal
                            }
                        }
                    }
                }
            }
            return false; // sin movimientos legales
        }

        // Verifica si hay jaque mate para el color especificado
        private bool EsJaqueMate(ColorPieza color)
        {
            bool reyEnJaque = tablero.ReyEnJaque(color);
            bool tieneMovimientos = TieneMovimientosLegales(color);
            
            return reyEnJaque && !tieneMovimientos;
        }

        // Verifica si hay ahogado para el color especificado
        private bool EsAhogado(ColorPieza color)
        {
            bool reyEnJaque = tablero.ReyEnJaque(color);
            bool tieneMovimientos = TieneMovimientosLegales(color);
            
            return !reyEnJaque && !tieneMovimientos;
        }

        // Verifica las condiciones de victoria después de un movimiento
        private void VerificarCondicionesDeVictoria()
        { 
            ColorPieza colorJugadorActual = (turno == Turno.Blancas) ? ColorPieza.Blanco : ColorPieza.Negro;
            
            bool reyEnJaque = tablero.ReyEnJaque(colorJugadorActual);
            bool tieneMovimientos = TieneMovimientosLegales(colorJugadorActual);
            
            if (EsJaqueMate(colorJugadorActual))
            {
                // el jugador que acaba de mover es el ganador
                string ganador = (turno == Turno.Blancas) ? "Negras" : "Blancas";
                ColorPieza colorGanador = (turno == Turno.Blancas) ? ColorPieza.Negro : ColorPieza.Blanco;
                Console.WriteLine($"JAQUE MATE! Las {ganador} ganan.");
                
                // notificar fin de partida
                FinPartida?.Invoke(colorGanador, false);
            }
            else if (EsAhogado(colorJugadorActual))
            {
                Console.WriteLine($"AHOGADO! La partida termina en empate.");
                
                //notificar fin de partida (empate)
                FinPartida?.Invoke(ColorPieza.Blanco, true); // Color no importa en empate
            }
        }

    }
}