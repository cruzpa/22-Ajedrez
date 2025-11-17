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
    public partial class Menu2 : Form
    {
        public Jugador jugadorBlancas;
        public Jugador jugadorNegras;
        
        private UI_LOGGED uI_LOGGED1; // Para jugador Blancas
        private UI_LOGGED uI_LOGGED2; // Para jugador Negras
        
        public Menu2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarLogins();
            ConfigurarTitulosLogin();
        }

        private void ConfigurarTitulosLogin()
        {
            uI_LOGIN1.ConfigurarTitulo("Jugador Blancas");
            uI_LOGIN2.ConfigurarTitulo("Jugador Negras");
        }
        
        private void ConfigurarLogins()
        {
            uI_LOGIN1.OnLoginSuccess += LoginBlancasCompletado;
            uI_LOGIN2.OnLoginSuccess += LoginNegrasCompletado;
        }
        
        private void LoginBlancasCompletado(Jugador jugador)
        {
            if (jugadorNegras != null && jugadorNegras.Id == jugador.Id)
            {
                MessageBox.Show("Este jugador ya esta logueado como Negras. Selecciona un jugador diferente.", "Jugador duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jugadorBlancas = jugador;
            Bitacora.RegistrarEvento(jugadorBlancas, Evento.LOGIN);
            
            // Guardar posición y tamaño antes de remover
            var location = uI_LOGIN1.Location;
            var size = uI_LOGIN1.Size;
            
            // Reemplazar UI_LOGIN1 con UI_LOGGED1
            this.Controls.Remove(uI_LOGIN1);
            uI_LOGGED1 = new UI_LOGGED(jugador);
            uI_LOGGED1.Location = location;
            uI_LOGGED1.Size = size;
            uI_LOGGED1.OnLogout += LogoutBlancasCompletado;
            this.Controls.Add(uI_LOGGED1);
            
            VerificarAmbosLogueados();
        }
        
        private void LoginNegrasCompletado(Jugador jugador)
        {
            if (jugadorBlancas != null && jugadorBlancas.Id == jugador.Id)
            {
                MessageBox.Show("Este jugador ya esta logueado como Blancas. Selecciona un jugador diferente.", "Jugador duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jugadorNegras = jugador;
            Bitacora.RegistrarEvento(jugadorNegras, Evento.LOGIN);

            // Guardar posición y tamaño antes de remover
            var location = uI_LOGIN2.Location;
            var size = uI_LOGIN2.Size;
            
            // Reemplazar UI_LOGIN2 con UI_LOGGED2
            this.Controls.Remove(uI_LOGIN2);
            uI_LOGGED2 = new UI_LOGGED(jugador);
            uI_LOGGED2.Location = location;
            uI_LOGGED2.Size = size;
            uI_LOGGED2.OnLogout += LogoutNegrasCompletado;
            this.Controls.Add(uI_LOGGED2);
            
            VerificarAmbosLogueados();
        }
        
        private void LogoutBlancasCompletado()
        {
            Bitacora.RegistrarEvento(jugadorBlancas, Evento.LOGOUT);
            jugadorBlancas = null;
            
            // Reemplazar UI_LOGGED1 con UI_LOGIN1
            if (uI_LOGGED1 != null)
            {
                this.Controls.Remove(uI_LOGGED1);
                uI_LOGGED1.Dispose();
                uI_LOGGED1 = null;
            }
            
            // Recrear UI_LOGIN1
            uI_LOGIN1 = new UI_LOGIN();
            uI_LOGIN1.Location = new System.Drawing.Point(27, 34);
            uI_LOGIN1.Size = new System.Drawing.Size(252, 381);
            uI_LOGIN1.ConfigurarTitulo("Jugador Blancas");
            uI_LOGIN1.OnLoginSuccess += LoginBlancasCompletado;
            this.Controls.Add(uI_LOGIN1);
            
            VerificarAmbosLogueados();
        }
        
        private void LogoutNegrasCompletado()
        {
            Bitacora.RegistrarEvento(jugadorNegras, Evento.LOGOUT);
            jugadorNegras = null;
            
            // Reemplazar UI_LOGGED2 con UI_LOGIN2
            if (uI_LOGGED2 != null)
            {
                this.Controls.Remove(uI_LOGGED2);
                uI_LOGGED2.Dispose();
                uI_LOGGED2 = null;
            }
            
            // Recrear UI_LOGIN2
            uI_LOGIN2 = new UI_LOGIN();
            uI_LOGIN2.Location = new System.Drawing.Point(480, 40);
            uI_LOGIN2.Size = new System.Drawing.Size(226, 374);
            uI_LOGIN2.ConfigurarTitulo("Jugador Negras");
            uI_LOGIN2.OnLoginSuccess += LoginNegrasCompletado;
            this.Controls.Add(uI_LOGIN2);
            
            VerificarAmbosLogueados();
        }
        
        private void VerificarAmbosLogueados()
        {
            if (jugadorBlancas != null && jugadorNegras != null)
            {
                Console.WriteLine("Ambos jugadores están listos");
                button1.Enabled = true;
                button1.Select();
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Menu2_Load(object sender, EventArgs e)
        {
            // Inicializar el botón como deshabilitado
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (jugadorBlancas == null || jugadorNegras == null)
            {
                MessageBox.Show("Ambos jugadores deben estar logueados para comenzar el juego.", 
                    "Jugadores incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Mesa mesa = new Mesa(jugadorBlancas, jugadorNegras);
            this.Hide();

            DialogResult resultado = mesa.ShowDialog();
            
            // Si "salir" entonces cerrar la aplicacion
            if (resultado == DialogResult.Cancel)
            {
                Application.Exit();
            }
            else
            {
                RefrescarJugadoresLogueados();
                this.Show();
            }
        }

        private void RefrescarJugadoresLogueados()
        {
            jugadorBlancas = ActualizarJugadorLogueado(jugadorBlancas, uI_LOGGED1);
            jugadorNegras = ActualizarJugadorLogueado(jugadorNegras, uI_LOGGED2);
        }

        private Jugador ActualizarJugadorLogueado(Jugador jugador, UI_LOGGED control)
        {
            Jugador jugadorActualizado = Jugador.Leer(jugador.Name, jugador.Pass);
            if (jugadorActualizado != null)
            {
                control.ActualizarJugador(jugadorActualizado);
                return jugadorActualizado;
            }

            return jugador;
        }
    }
}
