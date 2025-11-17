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
    public partial class FinPartida : Form
    {
        public Jugador JugadorGanador { get; set; }
        public Jugador JugadorPerdedor { get; set; }
        public TimeSpan TiempoJugado { get; set; }
        public bool EsEmpate { get; set; }
        
        public bool RevanchaSolicitada { get; private set; } = false;
        public bool VolverAlMenu { get; private set; } = false;

        public FinPartida()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void MostrarResultado()
        {
            if (EsEmpate)
            {
                labelTitulo.Text = "¡AHOGADO!";
                labelGanador.Text = "Empate";
                labelPerdedor.Text = "Empate";
            }
            else
            {
                labelTitulo.Text = "JAQUE MATE!";
                labelGanador.Text = $"Ganador: {JugadorGanador.Name}";
                labelPerdedor.Text = $"Perdedor: {JugadorPerdedor.Name}";
            }

            int horas = (int)TiempoJugado.TotalHours;
            int minutos = TiempoJugado.Minutes;
            int segundos = TiempoJugado.Seconds;
            labelTiempo.Text = $"Tiempo Jugado: {horas:D2}:{minutos:D2}:{segundos:D2}";
        }

        private void buttonRevancha_Click(object sender, EventArgs e)
        {
            RevanchaSolicitada = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            VolverAlMenu = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
