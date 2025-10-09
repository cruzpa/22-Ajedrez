using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        BE.Producto producto;
        BLL.Producto gestor = new BLL.Producto();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Enlazar();
        }

        public void Enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestor.Listar();

        }

        private void grabar()
        {
            if (producto == null)
            {
                producto = new BE.Producto();


            }
            producto.Nombre = textBox1.Text;
            producto.Precio = float.Parse(textBox2.Text);
            gestor.Grabar(producto);
            producto = null;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grabar();
            Enlazar();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            producto = dataGridView1.Rows[e.RowIndex].DataBoundItem as BE.Producto;

                textBox1.Text = producto.Nombre;
            textBox2.Text = producto.Precio.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (producto != null)
            {
                grabar();
                Enlazar();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (producto != null)
            {
                gestor.Borrar(producto);
                Enlazar();
            }
        }
    }
}
