using BE;
using System;
using System.Drawing;
using System.Windows.Forms;
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
                if (Casillero.Seleccionado)
                {
                    pictureBox1.BackColor = Color.LightCoral;
                }
                else
                {
                    pictureBox1.BackColor = Casillero.ColorFondo;
                }
            }
            else
            {
                pictureBox1.Image = null;
                pictureBox1.BackColor = Casillero.ColorFondo;
            }            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(Casillero.Seleccionado)
            {
                //Console.WriteLine($"deseleccione el casiilero: {Casillero}");
                Casillero.Seleccionado = false;
            }
            this.EnviarCasillero(Casillero);
            SetearImagen();
        }

        private void UI_CASILLERO_Click(object sender, EventArgs e)
        {
            
        }
    }
}
