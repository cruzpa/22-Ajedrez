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

        public Juego(Jugador blancas, Jugador negras)
        {
            this.Blancas = blancas;
            this.Negras = negras;
            this.tablero = new Tablero();
            this.tableroAux = new Tablero();
            this.turno = Turno.Blancas;
        }
    }
}