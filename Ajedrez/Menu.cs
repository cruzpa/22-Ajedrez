using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ajedrez
{
    public partial class Menu : Form
    {
        public Jugador jugadorBlancas;
        public Jugador jugadorNegras;
        public Mesa Mesa = new Mesa();

        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            jugadorBlancas = getJugador(userBlancas.Text, passBlancas.Text);
            infoLogueo(jugadorBlancas);


        }

        private Jugador getJugador(string username, string password)
        {
            if (!isEmpty(username) && !isEmpty(password))
            {
                return Jugador.Leer(username, password);
            }
            return null;
        }
        private bool isEmpty(string s)
        {
            return s == null || s.Length == 0;
        }

        private void infoLogueo(Jugador jugador)
        {
            if (jugador != null) 
            {
                MessageBox.Show("Jugador logueado correctamente");
                //Cambiar estado de botones, agregar desloguear; //volver a la pantalla anterior
            }
            else
            {
                MessageBox.Show("No existe el jugador.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //crear usuario..
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Mesa.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("img\\wk.png");
        }
    }
}
