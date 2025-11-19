using BE;
using DAL;

namespace BLL
{
    public static class Bitacora
    {
        public static void RegistrarEventoSesion(Jugador jugador, EventType evento)
        {
            MP_BITACORA.RegistrarEventoSesion(jugador, evento);

        }

        public static int RegistrarInicioPartida(Jugador jugadorBlancas, Jugador jugadorNegras)
        {
            return MP_BITACORA.RegistrarEventoInicioPartida(jugadorBlancas, jugadorNegras);
        }

        public static void RegistrarEventoFinPartida(int idPartida, int idJugadorBlancas, int idJugadorNegras, int idGanador, int idPerdedor, bool empate, int duracionSegundos)
        {
            MP_BITACORA.RegistrarEventoFinPartida(idPartida, idJugadorBlancas, idJugadorNegras, idGanador, idPerdedor, empate, duracionSegundos);
        }
    }
}
