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
        public JugadorHistorial Historial { get; set; }

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
            
            // Inicializar el objeto Historial con valores por defecto
            this.Historial = new JugadorHistorial
            {
                Id = NuevoId,
                Win = 0,
                Tie = 0,
                Loss = 0,
                TimePlayedSeconds = 0
            };
            
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
             
            SqlDataReader reader = Acceso.Leer($"select j.id, j.name, j.pass, jh.win, jh.tie, jh.loss, jh.time_played_seconds from jugador j inner join jugador_historial jh on j.id = jh.id where j.name='{name}' and j.pass='{pass}'");
            while (reader.Read())
            {
                jugador = new Jugador();
                int id = int.Parse(reader["id"].ToString());

                jugador.Id = id;
                jugador.Name = name;
                jugador.Pass = pass;

                // Crear y asignar el objeto JugadorHistorial
                jugador.Historial = new JugadorHistorial
                {
                    Id = id,
                    Win = int.Parse(reader["win"].ToString()),
                    Tie = int.Parse(reader["tie"].ToString()),
                    Loss = int.Parse(reader["loss"].ToString()),
                    TimePlayedSeconds = int.Parse(reader["time_played_seconds"].ToString())
                };
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

        public void ActualizarHistorial(bool gano, bool empato, int tiempoJugadoSegundos)
        {
            Acceso.Abrir();
            
            string campoActualizar = "";
            if (gano)
            {
                campoActualizar = "win = win + 1";
            }
            else if (empato)
            {
                campoActualizar = "tie = tie + 1";
            }
            else
            {
                campoActualizar = "loss = loss + 1";
            }
            
            string sql = $"update jugador_historial set {campoActualizar}, time_played_seconds = time_played_seconds + {tiempoJugadoSegundos} where id = {Id}";
            
            int resultado = Acceso.Escribir(sql);
            if (resultado > 0)
            {
                if (Historial != null)
                {
                    if (gano)
                    {
                        Historial.Win++;
                    }
                    else if (empato)
                    {
                        Historial.Tie++;
                    }
                    else
                    {
                        Historial.Loss++;
                    }
                    Historial.TimePlayedSeconds += tiempoJugadoSegundos;
                }
            }
            else
            {
                Console.WriteLine($"Error al actualizar historial del jugador {Name}");
            }
            
            Acceso.Cerrar();
        }

    }
}