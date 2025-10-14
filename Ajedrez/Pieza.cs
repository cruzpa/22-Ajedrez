using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez
{
    public abstract class Pieza: ICloneable
    {
        public string Imagen { get; set; }

        public ColorPieza Color { get; }

        protected Pieza(ColorPieza color)
        {
            Color = color;
        }

        public object Clone()
        {
            return this.MemberwiseClone(); //clonacion superficial del objeto
            //toma todos los datos y crea un objeto del mismo tipo con los mismos valores
        }

        public void Mover()
        {
            throw new System.NotImplementedException();
        }
    }
}