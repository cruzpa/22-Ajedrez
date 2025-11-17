using System;
using WindowsFormsApp1;

namespace Ajedrez
{
    public static class Bitacora
    {
        public static void RegistrarEvento(Jugador jugador, Evento evento)
        {

            var acceso = new Acceso();
            try
            {
                acceso.Abrir();

                int nuevoId = acceso.LeerEscalar("select isnull(max(id),0) + 1 from bitacora");

                string sql =
                    $"insert into bitacora (id, fecha, tipo_evento, id_jugador) values ({nuevoId}, GETDATE(), '{evento}', {jugador.Id})";
                int resultado = acceso.Escribir(sql);
                if (resultado <= 0)
                {
                    Console.WriteLine($"No se guardo el evento de bitacora {evento} para el jugador {jugador.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrando bitacora para el jugador {jugador?.Name}: {ex.Message}");
            }
            acceso.Cerrar();
            
        }

        public static int RegistrarInicioPartida(Jugador jugadorBlancas, Jugador jugadorNegras)
        {

            var acceso = new Acceso();

            try
            {
                acceso.Abrir();
                int nuevoRegistroId = acceso.LeerEscalar("select isnull(max(id),0) + 1 from bitacora");
                int nuevoIdPartida = acceso.LeerEscalar("select isnull(max(id_partida),0) + 1 from bitacora");

                string sql =
                    $"insert into bitacora (id, fecha, tipo_evento, id_partida, id_jugador_blancas, id_jugador_negras) " +
                    $"values ({nuevoRegistroId}, GETDATE(), '{Evento.PARTIDA_INICIO}', {nuevoIdPartida}, {jugadorBlancas.Id}, {jugadorNegras.Id})";

                int resultado = acceso.Escribir(sql);
                if (resultado <= 0)
                {
                    Console.WriteLine($"No se guardo el inicio de partida {nuevoIdPartida} en la bitacora.");
                    return -1;
                }

                acceso.Cerrar();
                return nuevoIdPartida;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrando el inicio de la partida: {ex.Message}");
                return -1;
            }
        }

        public static void RegistrarFinPartida(int idPartida, int idJugadorBlancas, int idJugadorNegras, int idGanador, int idPerdedor, bool empate, int duracionSegundos)
        {
            var acceso = new Acceso();
            try
            {
                acceso.Abrir();
                int nuevoRegistroId = acceso.LeerEscalar("select isnull(max(id),0) + 1 from bitacora");
                string sql = 
                    $"insert into bitacora (id, fecha, tipo_evento, id_partida, id_jugador_blancas, id_jugador_negras, id_ganador, id_perdedor, empate, duracion_segundos) " +
                    $"values ({nuevoRegistroId}, GETDATE(), '{Evento.PARTIDA_FIN}', {idPartida},{idJugadorBlancas}, {idJugadorNegras}, {idGanador}, {idPerdedor}, {(byte)(empate ? 1 : 0)}, {duracionSegundos})";
                
                
                int resultado = acceso.Escribir(sql);
                if (resultado <= 0)
                {
                    Console.WriteLine($"No se guardo el fin de partida {idPartida} en la bitacora.");
                }
                acceso.Cerrar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registrando el fin de la partida: {ex.Message}");
            }
        }
    }
}
