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
        public Tablero() { }
    
        public List<Casillero> casilleros = new List<Casillero>();

        public void InicializarTablero()
        {

            for (int y = 1; y <= 8; y++)
            {
                for (int x = 1; x <= 8; x++)
                {
                    Casillero casillero = new Casillero();
                    casillero.X = x;
                    casillero.Y = y;
                    casillero.Ancho = 60;
                    casillero.Pieza = SetPieza(x, y);
                    casillero.ColorFondo = ((x + y) % 2 == 0) ? Color.Beige : Color.SaddleBrown;

                    this.EnviarCasillero(casillero);
                    casilleros.Add(casillero);
                }
            }

        }

        private Pieza SetPieza(int x, int y)
        {
            // Fila 1 y 2 = negras
            if (y == 1)
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
            else if (y == 2)
            {
                return new Peon(ColorPieza.Negro);
            }

            // Fila 7 y 8 = blancas
            if (y == 7)
            {
                return new Peon(ColorPieza.Blanco);
            }
            else if (y == 8)
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

            return null; // casilla vacía
        }


    }
}