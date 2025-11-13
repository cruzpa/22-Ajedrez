using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Ajedrez
{
    public partial class UI_LOGIN : UserControl
    {

        public Jugador jugador;
        public UI_LOGIN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            jugador = getJugador(textBox1.Text, textBox2.Text);
            infoLogueo(jugador);
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

        private void button2_Click(object sender, EventArgs e)
        {
            jugador = crearJugador(textBox1.Text, textBox2.Text);
            infoRegistro(jugador);
        }

        private Jugador crearJugador(string username, string password)
        {
            if (!isEmpty(username) && !isEmpty(password))
            {
                jugador = new Jugador(username, password);
                jugador.Insertar();
                return jugador;

            }
            return null;
        }
        private void infoRegistro(Jugador jugador)
        {
            if (jugador != null)
            {
                MessageBox.Show("Jugador registrado correctamente");
                //Cambiar estado de botones, agregar desloguear; //volver a la pantalla anterior
            }
            else
            {
                MessageBox.Show("No fue posible crear al jugador.");
            }
        }
    }
}
