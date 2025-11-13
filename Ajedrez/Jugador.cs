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

        public Jugador(){}
        public Jugador(String Name, String Pass)
        {
            this.Name = Name;
            this.Pass = Pass;
        }

        public int Insertar()
        {
            Acceso.Abrir();
            int resultado = 0;
            if (Acceso.ExisteJugadorByName(Name))
            {
                Console.WriteLine("El jugador ya existe");
                resultado = -2;
                return resultado;
            }
            
            //en vez de manejar resultados, podria usar transacciones para hacer rollback automatico
            int NuevoId = Acceso.LeerEscalar($"select isnull(max(id),0) + 1 from jugador");
            resultado = Acceso.Escribir($"insert into jugador (id, name, pass) values ({NuevoId}, '{Name}', '{Pass}')");
            if (resultado <= 0)
            {
                Console.WriteLine("Error al insertar jugador");
                return -1;
            }

            resultado = Acceso.Escribir($"insert into jugador_historial (id, win, tie, loss, time_played_seconds) values ({NuevoId}, 0, 0, 0, 0)");
            if (resultado <= 0)
            {
                try
                {
                    Acceso.Escribir($"delete from jugador where id = {NuevoId}");
                }
                catch
                {
                    Console.WriteLine("Fallo al insertar historial y al intentar revertir la inserción del jugador.");
                }

                Console.WriteLine("Error al insertar historial. Se revierte la inserción.");
                return -1;
            }

            
            Acceso.Cerrar();
            this.Id = NuevoId;
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