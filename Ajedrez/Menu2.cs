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
    public partial class Menu2 : Form
    {
        public Jugador jugadorBlancas;
        public Jugador jugadorNegras;
        public Mesa Mesa = new Mesa();
        public Menu2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //Mesa.ShowDialog();
        }

        private void Menu2_Load(object sender, EventArgs e)
        {

        }
    }
}
