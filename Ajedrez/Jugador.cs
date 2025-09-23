using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WindowsFormsApp1;

namespace Ajedrez
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }

        private Acceso Acceso = new Acceso();

        public int Insertar()
        {
            Acceso.Abrir();
            int resultado = 0;
            if (Acceso.ExisteJugadorByName(Name))
            {
                Console.WriteLine("El jugador ya existe");
                resultado = -2;
            }
            else
            {
                int NuevoId = Acceso.LeerEscalar($"select isnull(max(id),0) + 1 from jugador");
                resultado = Acceso.Escribir($"insert into jugador (id, name, pass) values ({NuevoId}, '{Name}', '{Pass}')");
            }
            Acceso.Cerrar();
            return resultado;
        }

        public static Jugador Leer(string name, string pass)
        {
            Jugador jugador = null;
            Acceso Acceso = new Acceso();
            Acceso.Abrir();
            if( !Acceso.ExisteJugadorByName(name))
            {
                Console.WriteLine("El jugador no existe");
                //aca podria tirar una excepcion y atajarla arriba
                return null;
            }
             
            SqlDataReader reader = Acceso.Leer($"select id, name, pass from jugador where name='{name}' and pass='{pass}'");
            while (reader.Read())
            {
                jugador = new Jugador();
                int id = int.Parse(reader["id"].ToString());

                jugador.Id = id;
                jugador.Name = name;
                jugador.Pass = pass;
            }
            reader.Close();
            Acceso.Cerrar();

            if(jugador == null)
            {
                Console.WriteLine("La contraseña es incorrecta");
                //aca tirar otra excepcion.
            }

            return jugador;
        }
    }
}