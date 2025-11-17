using System;
using System.Windows.Forms;

namespace Ajedrez
{
    public partial class UI_LOGIN : UserControl
    {
        public event Action<Jugador> OnLoginSuccess;
        public UI_LOGIN()
        {
            InitializeComponent();
        }

        public void ConfigurarTitulo(string titulo)
        {
            label1.Text = titulo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var jugador = getJugador(textBox1.Text, textBox2.Text);

            if (jugador != null)
            {
                OnLoginSuccess?.Invoke(jugador);
            }
            else
            {
                MessageBox.Show("No existe el jugador.");
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            var jugador = crearJugador(textBox1.Text, textBox2.Text);

            if (jugador != null)
            {
                OnLoginSuccess?.Invoke(jugador);
            }
            else
            {
                MessageBox.Show("No fue posible crear el jugador.");
            }
        }

        private Jugador crearJugador(string username, string password)
        {
            if (!isEmpty(username) && !isEmpty(password))
            {
                Jugador jugador = new Jugador(username, password);
                int resultado = jugador.Insertar();
                if (resultado >= 0 )
                {
                    return jugador;
                }
            }
            return null;
        }
    }
}
