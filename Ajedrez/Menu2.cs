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
        public Menu2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarLogin();
            //Mesa.ShowDialog();
        }
        private void CargarLogin()
        {
            var login = new UI_LOGIN();

            // Suscribirse al evento
            login.OnLoginSuccess += LoginCompletado;

            CambiarVista(login);
        }
        private void LoginCompletado(Jugador jugador)
        {
            var logged = new UI_LOGGED(jugador);

            // Suscribirse al evento de logout
            logged.OnLogout += LogoutCompletado;

            CambiarVista(logged);
        }
        private void LogoutCompletado()
        {
            CargarLogin();
        }

        private void Menu2_Load(object sender, EventArgs e)
        {

        }

        private void CambiarVista(UserControl vista)
        {
            this.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            this.Controls.Add(vista);
        }
    }
}
