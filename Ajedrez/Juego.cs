using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Juego
    {

        public Tablero tablero;
        public Tablero tableroAux;

        public Jugador Blancas;
        public Jugador Negras;

        public Turno turno;

        public Casillero casilleroPrevio = null;

        public Juego(Tablero tablero) {
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
            //deselecciono
            if (casilleroPrevio != null && casilleroPrevio.Equals(casillero))
            {
                casilleroPrevio = null; 
                return;
            }

            //no comparo pq no tengo contra quien.
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

                    //ActivarEnPassantSiCorresponde(casillero);
                    casilleroPrevio = null;


                    Console.WriteLine($"destino: {casillero}");
                }
                else
                {
                    Console.WriteLine($"movimiento invalido: {casilleroPrevio} a {casillero}");
                }

            }
            //si movimiento valido -> pasar turno
            //turno = (turno == Turno.Blancas) ? Turno.Negras : Turno.Blancas;



        }

        private void ActivarEnPassantSiCorresponde(Casillero casillero)
        {
            if (casillero.Pieza is Peon peon)
            {
                peon.RecienMovidoDoble = Math.Abs(casillero.Y - casilleroPrevio.Y) == 2;
            }
        }

        private void DesactivarEnPassant()
        {
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
            if (origen.Pieza == null)
                return false;

            // No permitir mover a una casilla ocupada por una pieza del mismo color
            if (destino.Pieza != null && destino.Pieza.Color == origen.Pieza.Color)
                return false;

            // Delegar la validación a la pieza
            return origen.Pieza.PuedeMover(tablero, origen, destino);
        }
    }
}