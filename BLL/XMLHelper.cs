using BE;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class XMLHelper
    {

        XMLManager xMLManager = new XMLManager();
        public void GuardarPartida(GameHistory gameHistory)
        {
            xMLManager.GuardarPartidaEnHistorial(gameHistory);
        }

        public List<GameHistory> LeerHistorial()
        {
            return xMLManager.LeerHistorialConSchema();
        }
    }
}
