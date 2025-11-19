using DAL;
using System;

namespace BE
{
    public class JugadorBLL
    {
        MP_JUGADOR jugadorMapper = new MP_JUGADOR();
        public int Insertar(Jugador jugador)
        {
            return jugadorMapper.Insertar(jugador);
        }

        public Jugador Leer(Jugador jugador)
        {
            return jugadorMapper.Leer(jugador.Name, jugador.Pass);
        }

        public String Leer(int id)
        {
            return jugadorMapper.LeerName(id);
        }


        public void ActualizarHistorial(int jugadorId, bool gano, bool empato, int tiempoJugadoSegundos)
        {
            jugadorMapper.ActualizarHistorial(jugadorId, gano, empato, tiempoJugadoSegundos);

        }
    }
}