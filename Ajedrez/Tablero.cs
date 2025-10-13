using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Tablero
    { 
        public event DelEnviarCasillero EnviarCasillero;
        public Tablero() { }
    
        public List<Casillero> casilleros = new List<Casillero>();
        List<Pieza> piezas = new List<Pieza>();

        public void InicializarTablero()
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Casillero casillero = new Casillero();
                    casillero.X = i;
                    casillero.Y = j;
                    casillero.Ancho = 60;
                    casillero.Pieza = new Rey();
                    this.EnviarCasillero(casillero);
                    casilleros.Add(casillero);
                }
            }

        }

    }
}