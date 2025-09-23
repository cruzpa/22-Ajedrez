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
    public partial class Login : Form
    {

        public Jugador jugador;
        public bool userLogguedSuccess;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isEmpty(textBox1.Text) && !isEmpty(textBox2.Text))
            {
                jugador = Jugador.Leer(textBox1.Text, textBox2.Text);
                if (jugador != null)
                {
                    userLogguedSuccess = true;
                    MessageBox.Show("Jugador logueado correctamente");
                    Limpiar(); //volver a la pantalla anterior
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No existe el jugador.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!isEmpty(textBox1.Text) && !isEmpty(textBox2.Text))
            {
                jugador = new Jugador();
                jugador.Name = textBox1.Text;
                jugador.Pass = textBox2.Text;
                int resultado = jugador.Insertar();
                jugador = null;
                if(resultado > 0)
                {
                    MessageBox.Show("Jugador registrado");
                    Limpiar(); //volver a la pantalla anterior
                }
                else if (resultado == -2)
                {
                    MessageBox.Show("El jugador ya existe");
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Error al registrar jugador.");
                }
            }
        }

        private bool isEmpty(string s)
        {
            return s == null || s.Length == 0;
        }

        private void Limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
