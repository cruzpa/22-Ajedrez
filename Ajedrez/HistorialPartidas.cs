using BE;
using System.Windows.Forms;

namespace Ajedrez
{
    public partial class HistorialPartidas : Form
    {
        public HistorialPartidas(Jugador jugador)
        {
            InitializeComponent();
            Enlazar();
        }

        public void Enlazar()
        {
            //var partidas = ListarPartidas(jugador);
            //comboBox1.DataSource = partidas;
        }
    }
}
