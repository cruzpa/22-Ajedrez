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
        public Mesa Mesa = new Mesa();
        
        private UI_LOGGED uI_LOGGED1; // Para jugador Blancas
        private UI_LOGGED uI_LOGGED2; // Para jugador Negras
        
        public Menu2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarLogins();
        }
        
        private void ConfigurarLogins()
        {
            // Configurar login para jugador Blancas (uI_LOGIN1)
            uI_LOGIN1.OnLoginSuccess += LoginBlancasCompletado;
            
            // Configurar login para jugador Negras (uI_LOGIN2)
            uI_LOGIN2.OnLoginSuccess += LoginNegrasCompletado;
        }
        
        private void LoginBlancasCompletado(Jugador jugador)
        {
            jugadorBlancas = jugador;
            
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
            jugadorNegras = jugador;
            
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
            uI_LOGIN1.OnLoginSuccess += LoginBlancasCompletado;
            this.Controls.Add(uI_LOGIN1);
        }
        
        private void LogoutNegrasCompletado()
        {
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
            uI_LOGIN2.OnLoginSuccess += LoginNegrasCompletado;
            this.Controls.Add(uI_LOGIN2);
        }
        
        private void VerificarAmbosLogueados()
        {
            if (jugadorBlancas != null && jugadorNegras != null)
            {
                Console.WriteLine("Ambos jugadores están listos");
            }
        }

        private void Menu2_Load(object sender, EventArgs e)
        {

        }
    }
}
