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
    public partial class Mesa : Form
    {
        public Tablero tablero;
        public Juego juego;
        public Jugador JugadorBlancas { get; private set; }
        public Jugador JugadorNegras { get; private set; }
        
        private DateTime tiempoInicio;
        private bool partidaIniciada = false;
        
        public Mesa(Jugador jugadorBlancas, Jugador jugadorNegras)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(600, 600);
            this.MinimumSize = new Size(600, 600);
            
            this.JugadorBlancas = jugadorBlancas;
            this.JugadorNegras = jugadorNegras;
            
            juego = new Juego(jugadorBlancas, jugadorNegras);
            tablero = juego.tablero;
            
            juego.FinPartida += Juego_FinPartida;
            
            
            tablero.EnviarCasillero += Tablero_EnviarCasillero;
        }

        private void Tablero_EnviarCasillero(Casillero casillero)
        {
            int separacion = 0;
            int totalFilas = 9;

            UI_CASILLERO ui_casillero = new UI_CASILLERO();
            ui_casillero.Location = new Point(
                (casillero.X) * (casillero.Ancho + separacion),
                (totalFilas - casillero.Y) * (casillero.Ancho + separacion)
                );
            ui_casillero.Size = new Size(casillero.Ancho, casillero.Ancho);
            ui_casillero.Casillero = casillero;
            ui_casillero.EnviarCasillero += Cas_EnviarCasillero;

            this.Controls.Add(ui_casillero);
        }

        private void Cas_EnviarCasillero(Casillero casillero)
        {
            comenzarTiempoSiPrimerMovimiento();

            juego.CompararCasillero(casillero);

            //actualizar tablerinho
            foreach (Control control in this.Controls)
            {
                if (control is UI_CASILLERO ui_casillero)
                {
                    ui_casillero.SetearImagen();
                }
            }
        }

        private void comenzarTiempoSiPrimerMovimiento()
        {
            if (!partidaIniciada)
            {
                tiempoInicio = DateTime.Now;
                partidaIniciada = true;
            }
        }

        private void Juego_FinPartida(ColorPieza colorGanador, bool esEmpate)
        {

            TimeSpan tiempoJugado = DateTime.Now - tiempoInicio;
            
            // Determinar ganador y perdedor
            Jugador ganador = null;
            Jugador perdedor = null;
            
            if (!esEmpate)
            {
                if (colorGanador == ColorPieza.Blanco)
                {
                    ganador = JugadorBlancas;
                    perdedor = JugadorNegras;
                }
                else
                {
                    ganador = JugadorNegras;
                    perdedor = JugadorBlancas;
                }
            }
            

            FinPartida finPartida = new FinPartida();
            finPartida.JugadorGanador = ganador;
            finPartida.JugadorPerdedor = perdedor;
            finPartida.TiempoJugado = tiempoJugado;
            finPartida.EsEmpate = esEmpate;
            finPartida.MostrarResultado();
            
            DialogResult resultado = finPartida.ShowDialog();
            
            //procesar decision del usuario
            if (finPartida.RevanchaSolicitada)
            {
                ReiniciarPartida();
            }
            else if (finPartida.VolverAlMenu)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        private void ReiniciarPartida()
        {
            // Limpiar controles del tablero
            foreach (Control control in this.Controls)
            {
                if (control is UI_CASILLERO)
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }
            juego = new Juego(JugadorBlancas, JugadorNegras);
            tablero = juego.tablero;
            
            tablero.EnviarCasillero += Tablero_EnviarCasillero;
            juego.FinPartida += Juego_FinPartida;
            
            partidaIniciada = false;
            tablero.InicializarTablero();
        }

        private void Mesa_Shown(object sender, EventArgs e)
        {
            tablero.InicializarTablero();
        }
    }
}
