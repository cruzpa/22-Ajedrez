using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Producto
    {
        DAL.MP_PRODUCTO mp = new DAL.MP_PRODUCTO();
        public void Grabar(BE.Producto producto)
        {
            if (producto.Id == 0)
            {
                mp.Insertar(producto);
            } else
            {
                mp.Editar(producto);
            }
        }

        public void Borrar(BE.Producto producto)
        {
            mp.Borrar(producto);
        }

        public List<BE.Producto> Listar()
        {
            return mp.Listar();
        }
    }
}