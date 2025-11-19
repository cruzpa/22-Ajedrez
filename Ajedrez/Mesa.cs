using BE;
using BLL;
using System;
using System.Drawing;
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
        private int IdPartida { get; set; }

        private JugadorBLL jugadorBLL = new JugadorBLL();

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

            IdPartida = Bitacora.RegistrarInicioPartida(jugadorBlancas, jugadorNegras);
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

        private void Juego_FinPartida(ColorPieza colorGanador, bool esEmpate, Jugador Blancas, Jugador Negras)
        {

            TimeSpan tiempoJugado = DateTime.Now - tiempoInicio;
            int tiempoJugadoSegundos = (int)tiempoJugado.TotalSeconds;

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

            updateHistorial(esEmpate, tiempoJugadoSegundos, ganador, perdedor);

            Bitacora.RegistrarEventoFinPartida(IdPartida, Blancas.Id, Negras.Id, ganador.Id, perdedor.Id, esEmpate, tiempoJugadoSegundos);

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
            else if (resultado == DialogResult.Cancel)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void updateHistorial(bool esEmpate, int tiempoJugadoSegundos, Jugador ganador, Jugador perdedor)
        {
            if (esEmpate)
            {;
                jugadorBLL.ActualizarHistorial(JugadorBlancas.Id, false, true, tiempoJugadoSegundos);
                jugadorBLL.ActualizarHistorial(JugadorNegras.Id, false, true, tiempoJugadoSegundos);
            }
            else
            {

                jugadorBLL.ActualizarHistorial(ganador.Id, true, false, tiempoJugadoSegundos);
                jugadorBLL.ActualizarHistorial(perdedor.Id, false, false, tiempoJugadoSegundos);
            }
        }

        private void ReiniciarPartida()
        {
            // Ocultar la mesa actual
            this.Hide();
            
            // Crear nueva mesa con los mismos jugadores
            Mesa nuevaMesa = new Mesa(JugadorBlancas, JugadorNegras);
            
            // Mostrar la nueva mesa
            nuevaMesa.ShowDialog();
            
            // Cuando la nueva mesa se cierre, cerrar también esta
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Mesa_Shown(object sender, EventArgs e)
        {
            tablero.InicializarTablero();
        }
    }
}
