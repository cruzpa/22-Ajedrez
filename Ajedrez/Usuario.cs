using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WindowsFormsApp1;

namespace Ajedrez
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }

        private Acceso Acceso = new Acceso();

        public int Insertar()
        {
            Acceso.Abrir();
            int resultado = 0;
            if (Acceso.ExisteUsuarioByName(Name))
            {
                Console.WriteLine("El usuario ya existe");
                resultado = -2;
            }
            else
            {
                int NuevoId = Acceso.LeerEscalar($"select isnull(max(id),0) + 1 from usuario");
                resultado = Acceso.Escribir($"insert into usuario (id, name, pass) values ({NuevoId}, '{Name}', '{Pass}')");
            }
            Acceso.Cerrar();
            return resultado;
        }

        public static Usuario Leer(string name, string pass)
        {
            Usuario usuario = null;
            Acceso Acceso = new Acceso();
            Acceso.Abrir();
            if( !Acceso.ExisteUsuarioByName(name))
            {
                Console.WriteLine("El usuario no existe");
                //aca podria tirar una excepcion y atajarla arriba
                return null;
            }

            SqlDataReader reader = Acceso.Leer($"select id, name, pass from usuario where name='{name}' and pass='{pass}'");
            while (reader.Read())
            {
                usuario = new Usuario();
                int id = int.Parse(reader["id"].ToString());

                usuario.Id = id;
                usuario.Name = name;
                usuario.Pass = pass;
            }
            reader.Close();
            Acceso.Cerrar();

            if(usuario == null)
            {
                Console.WriteLine("La contraseña es incorrecta");
                //aca tirar otra excepcion.
            }

            return usuario;
        }
    }
}