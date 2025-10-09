using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MP_PRODUCTO : Mapper<BE.Producto>

    {
        public override int Borrar(Producto obj)
        {
            acceso = new Acceso();
            acceso.Abrir();
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(acceso.CrearParametro("@id", obj.Id));
            int res = acceso.Escribir("PRODUCTO_BORRAR", sp);
            acceso.Cerrar();
            return res;
        }

        public override int Editar(Producto obj)
        {
            acceso = new Acceso();
            acceso.Abrir();
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(acceso.CrearParametro("@id", obj.Id));
            sp.Add(acceso.CrearParametro("@nombre", obj.Nombre));
            sp.Add(acceso.CrearParametro("@precio", obj.Precio));
            int res = acceso.Escribir("PRODUCTO_EDITAR", sp);
            acceso.Cerrar();
            return res;
        }

        public override int Insertar(Producto obj)
        {
            acceso = new Acceso();
            acceso.Abrir();
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(acceso.CrearParametro("@id", obj.Id));
            sp.Add(acceso.CrearParametro("@nombre", obj.Nombre));
            sp.Add(acceso.CrearParametro("@precio", obj.Precio));
            int res = acceso.Escribir("PRODUCTO_INSERTAR", sp);
            acceso.Cerrar();
            return res;
        }

        public override List<Producto> Listar()
        {
            List<Producto> productos = new List<Producto>();


            acceso = new Acceso();
            acceso.Abrir();
            System.Data.DataTable dataTable = acceso.Leer("PRODUCTO_LISTAR");
            foreach (DataRow row in dataTable.Rows)
            {
                Producto producto = new Producto();
                producto.Id = int.Parse(row["ID_PRODUCTO"].ToString());
                producto.Nombre = row["nombre"].ToString();
                producto.Precio = float.Parse(row["precio"].ToString());
            }
            
            
            acceso.Cerrar();
            return productos;
        }
    }
}