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
        public Usuario usuarioBlancas;
        public Usuario usuarioNegras;

        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            if (login.usuario != null && login.userLogguedSuccess)
            {
                usuarioBlancas = login.usuario;
                login.usuario = null;
            }

            this.Show();
            button1.Text = usuarioBlancas != null ? usuarioBlancas.Name : "Jugador1 Ingresar!";
        }

        private void userBlancas_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
