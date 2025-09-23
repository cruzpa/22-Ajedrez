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
    public partial class Menu : Form
    {
        public Jugador jugadorBlancas;
        public Jugador jugadorNegras;

        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            if (login.jugador != null && login.userLogguedSuccess)
            {
                jugadorBlancas = login.jugador;
                login.jugador = null;
            }

            this.Show();
            button1.Text = jugadorBlancas != null ? jugadorBlancas.Name : "Jugador1 Ingresar!";
        }
    }
}
