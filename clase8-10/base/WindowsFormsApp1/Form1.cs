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
    }
}
