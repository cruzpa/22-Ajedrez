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
            if (Casillero.Pieza != null)
            {
                pictureBox1.Image = Image.FromFile(Casillero.Pieza.Imagen);
                pictureBox1.BackColor = Casillero.ColorFondo;
            }
            else
            {
                pictureBox1.Image = null;
                pictureBox1.BackColor = Casillero.ColorFondo;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(Casillero.Pieza == null) return; 
            

            Casillero.Seleccionado = !Casillero.Seleccionado;

            if (Casillero.Seleccionado)
            {
                Console.WriteLine($"seleccione el casiilero: {Casillero.X},{Casillero.Y}");
                pictureBox1.BackColor = Color.LightCoral;
            }
            else
            {
                Console.WriteLine($"deseleccione el casiilero:: {Casillero.X},{Casillero.Y}");
                pictureBox1.BackColor = Casillero.ColorFondo;
            }

            this.EnviarCasillero(Casillero);
        }

        private void UI_CASILLERO_Click(object sender, EventArgs e)
        {
            
        }
    }
}
