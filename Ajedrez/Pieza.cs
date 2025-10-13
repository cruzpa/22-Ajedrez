using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public abstract class Pieza
    {
        public string Imagen { get; set; }

        public void Mover()
        {
            throw new System.NotImplementedException();
        }
    }
}