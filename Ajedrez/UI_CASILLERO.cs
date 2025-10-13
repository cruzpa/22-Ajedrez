using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Ajedrez
{
    public partial class UI_CASILLERO : UserControl
    {
        public event DelEnviarCasillero EnviarCasillero;
        private Casillero _casillero;
        public Casillero Casillero
        {
            get { return _casillero; }
            set
            {
                _casillero = value;
                SetearImagen();
            }
        }


        public UI_CASILLERO()
        {
            InitializeComponent();
        }

        private void UI_CASILLERO_Load(object sender, EventArgs e)
        {

        }

        public void SetearImagen()
        {
            if(Casillero.Pieza.Imagen != null)
            {

                //pictureBox1.BackColor = Color.Red;
                pictureBox1.Image = Image.FromFile(Casillero.Pieza.Imagen);
            } else
            {
                pictureBox1.BackColor = Color.Red;
            }
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
