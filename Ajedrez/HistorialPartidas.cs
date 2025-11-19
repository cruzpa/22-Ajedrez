using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
