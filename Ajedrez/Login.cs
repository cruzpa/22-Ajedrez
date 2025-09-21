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

        public Usuario usuario;
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

                usuario = Usuario.Leer(textBox1.Text, textBox2.Text);

                if (usuario != null)
                {
                    userLogguedSuccess = true;
                    MessageBox.Show("Usuario logueado correctamente");
                    Limpiar(); //volver a la pantalla anterior
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No existe el usuario.");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!isEmpty(textBox1.Text) && !isEmpty(textBox2.Text))
            {
                usuario = new Usuario();
                usuario.Name = textBox1.Text;
                usuario.Pass = textBox2.Text;

                int resultado = usuario.Insertar();
                usuario = null;
                if(resultado > 0)
                {
                    MessageBox.Show("Usuario registrado");
                    Limpiar(); //volver a la pantalla anterior
                }
                else if (resultado == -2)
                {
                    MessageBox.Show("El usuario ya existe");
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Error al registrar usuario.");
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
