using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Ajedrez
{
    public partial class UI_LOGIN : UserControl
    {
        public event Action<Jugador> OnLoginSuccess;
        public Jugador jugador { get; set; }
        public JugadorBLL jugadorBLL = new JugadorBLL();
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
            jugador = getJugador(textBox1.Text, textBox2.Text);

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
                Jugador jugador = new Jugador(username, password);
                return jugadorBLL.Leer(jugador);
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
                int resultado = jugadorBLL.Insertar(jugador);
                if (resultado >= 0 )
                {
                    return jugador;
                }
            }
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            HistorialPartidas historial = new HistorialPartidas(jugador);
            DialogResult resultado = historial.ShowDialog();
            this.Show();
            
        }
    }
}
