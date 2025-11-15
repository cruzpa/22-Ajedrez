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
    public partial class UI_LOGGED : UserControl
    {
        public event Action OnLogout;
        private Jugador jugador;
        public UI_LOGGED(Jugador jugador)
        {
            InitializeComponent();
            this.jugador = jugador;

            label1.Text = $"Jugador: {jugador.Name}";
            
            if (jugador.Historial != null)
            {
                int totalPartidas = jugador.Historial.Win + jugador.Historial.Tie + jugador.Historial.Loss;
                double porcentajeVictorias = totalPartidas > 0 
                    ? Math.Round((double)jugador.Historial.Win / totalPartidas * 100, 1) 
                    : 0;
                
                label2.Text = $"% Victorias: {porcentajeVictorias}%";
                
                int horas = jugador.Historial.TimePlayedSeconds / 3600;
                int minutos = (jugador.Historial.TimePlayedSeconds % 3600) / 60;
                int segundos = jugador.Historial.TimePlayedSeconds % 60;
                string tiempoFormateado = $"{horas:D2}:{minutos:D2}:{segundos:D2}";
                
                label3.Text = $"Tiempo jugado: {tiempoFormateado}";
                label4.Text = $"Victorias: {jugador.Historial.Win}";
                label5.Text = $"Empates: {jugador.Historial.Tie}";
                label6.Text = $"Derrotas: {jugador.Historial.Loss}";
            }
            else
            {
                label2.Text = "% Victorias: 0%";
                label3.Text = "Tiempo jugado: 00:00:00";
                label4.Text = "Victorias: 0";
                label5.Text = "Empates: 0";
                label6.Text = "Derrotas: 0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnLogout?.Invoke();
        }
    }
}
