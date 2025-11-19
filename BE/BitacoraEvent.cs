using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BitacoraEvent
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public EventType TipoEvento { get; set; }

        public int IdJugador { get; set; }
        public int IdPartida { get; set; }

        public int IdJugadorBlancas { get; set; }
        public int IdJugadorNegras { get; set; }

        public int IdGanador { get; set; }
        public int IdPerdedor { get; set; }

        public bool Empate { get; set; }
        public int DuracionSegundos { get; set; }

    }
}
