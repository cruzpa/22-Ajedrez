using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ajedrez
{
    public class Tablero
    { 
        public event DelEnviarCasillero EnviarCasillero;
        private Casillero[,] casilleros = new Casillero[9, 9];
        public Tablero() { }

        public void InicializarTablero()
        {
            int ancho = 60;
            for (int y = 8; y >= 1; y--) // filas de 8 a 1
            {
                for (int x = 1; x <= 8; x++) // columnas de 1 a 8
                {
                    Casillero casillero = new Casillero();
                    casillero.X = x;
                    casillero.Y = y;
                    casillero.Ancho = ancho;
                    casillero.Pieza = SetPieza(x, y);
                    casillero.ColorFondo = ((x + y) % 2 == 0) ? Color.Beige : Color.SaddleBrown;

                    casilleros[x, y] = casillero;
                    Console.WriteLine(casillero.ToString());

                    this.EnviarCasillero(casillero);
                }
            }

        }

        private Pieza SetPieza(int x, int y)
        {
            // Fila 1 y 2 = Blancas
            if (y == 1)
            {
                switch (x)
                {
                    case 1: case 8: return new Torre(ColorPieza.Blanco);
                    case 2: case 7: return new Caballo(ColorPieza.Blanco);
                    case 3: case 6: return new Alfil(ColorPieza.Blanco);
                    case 4: return new Reyna(ColorPieza.Blanco);
                    case 5: return new Rey(ColorPieza.Blanco);
                }
            }
            else if (y == 2)
            {
                return new Peon(ColorPieza.Blanco);
            }

            // Fila 7 y 8 = Negras
            if (y == 7)
            {
                return new Peon(ColorPieza.Negro);
            }
            else if (y == 8)
            {
                switch (x)
                {
                    case 1: case 8: return new Torre(ColorPieza.Negro);
                    case 2: case 7: return new Caballo(ColorPieza.Negro);
                    case 3: case 6: return new Alfil(ColorPieza.Negro);
                    case 4: return new Reyna(ColorPieza.Negro);
                    case 5: return new Rey(ColorPieza.Negro);
                }
            }

            return null; // casilla vacía
        }

        public Casillero GetCasillero(int x, int y)
        {
            if (x <= 0 || x > 8 || y <= 0 || y > 8)
                throw new ArgumentOutOfRangeException("Coordenadas fuera del rango del tablero.");

            return casilleros[x, y];
        }


        // Devuelve true si el rey del color indicado esta en jaque (alguna pieza rival puede moverse a su casillero)
        public bool ReyEnJaque(ColorPieza color)
        {
            // Buscar la posicion del rey del color indicado
            Casillero casilleroRey = ObtenerCasilleroRey(color);

            // Recorrer todas las piezas rivales y verificar si alguna puede mover al casillero del rey
            return RivalPuedeMoverAlRey(color, casilleroRey);
        }

        private bool RivalPuedeMoverAlRey(ColorPieza color, Casillero casilleroRey)
        {
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    var origen = casilleros[x, y];
                    if (origen == null || origen.Pieza == null)
                        continue;

                    if (origen.Pieza.Color != color)
                    {
                        if (origen.Pieza.PuedeMover(this, origen, casilleroRey))
                        {
                            // Una pieza rival puede capturar al rey -> esta en jaque
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private Casillero ObtenerCasilleroRey(ColorPieza color)
        {
            Casillero casilleroRey = null;
            for (int x = 1; x <= 8 && casilleroRey == null; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    var c = casilleros[x, y];
                    if (c != null && c.Pieza is Rey rey && rey.Color == color)
                    {
                        casilleroRey = c;
                        break;
                    }
                }
            }

            return casilleroRey;
        }
    }
}