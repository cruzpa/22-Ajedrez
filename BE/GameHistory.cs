using System;
using System.Collections.Generic;

namespace BE
{
    public class GameHistory
    {
        public DateTime Fecha { get; set; }
        public int IdPartida { get; set; }
        public int IdBlancas { get; set; }
        public int IdNegras { get; set; }
        public int IdGanador { get; set; }
        public int IdPerdedor { get; set; }
        public bool Empate { get; set; }
        public int DuracionSegundos { get; set; }
        public List<string> Movimientos { get; set; }

        public GameHistory()
        {
            Movimientos = new List<string>();
        }
    }
}