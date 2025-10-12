using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public class Tablero
    { 
        public Tablero() { }
    
        //necesito que tenga una matriz de casilleros 8x8
        public Casillero[,] casilleros = new Casillero[8, 8];

    }
}