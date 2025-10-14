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

        public Juego() { }
        public Juego(Jugador blancas, Jugador negras)
        {
            this.Blancas = blancas;
            this.Negras = negras;
            this.tablero = new Tablero();
            this.tableroAux = new Tablero();
            this.turno = Turno.Blancas;
        }

        public void CompararCasillero(Casillero casillero)
        {
            if (casilleroPrevio == null && casillero.Pieza != null)
            {
                //no comparo pq no tengo contra quien.
                casillero.Seleccionado = true;
                casilleroPrevio = casillero;
                return;
            }

            if (casilleroPrevio != null)
            {
                casillero.Pieza = casilleroPrevio.Pieza;
                casilleroPrevio.Pieza = null;
                casilleroPrevio.Seleccionado = false;
            }

            //si movimiento valido -> pasar turno
            turno = (turno == Turno.Blancas) ? Turno.Negras : Turno.Blancas;
            casilleroPrevio = null;


        }
    }
}