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
    public partial class Mesa : Form
    {
        public Tablero tablero;
        public Juego juego;
        public Mesa()
        {
            InitializeComponent();
            this.ClientSize = new Size(600, 600);
            this.MinimumSize = new Size(600, 600);
            tablero = new Tablero();
            juego = new Juego(tablero);

            tablero.EnviarCasillero += Tablero_EnviarCasillero;
        }

        private void Tablero_EnviarCasillero(Casillero casillero)
        {
            int separacion = 0;
            int totalFilas = 9; //para que quede centrado

            UI_CASILLERO ui_casillero = new UI_CASILLERO();
            ui_casillero.Location = new Point(
                (casillero.X) * (casillero.Ancho + separacion),
                (totalFilas - casillero.Y) * (casillero.Ancho + separacion)
                );
            ui_casillero.Size = new Size(casillero.Ancho, casillero.Ancho);
            ui_casillero.Casillero = casillero;
            ui_casillero.EnviarCasillero += Cas_EnviarCasillero;

            this.Controls.Add(ui_casillero);
        }

        private void Cas_EnviarCasillero(Casillero casillero)
        {

            juego.CompararCasillero(casillero);

            //actualizar tablerinho
            foreach (Control control in this.Controls)
            {
                if (control is UI_CASILLERO ui_casillero)
                {
                    ui_casillero.SetearImagen();
                }
            }
        }

        private void Mesa_Shown(object sender, EventArgs e)
        {
            tablero.InicializarTablero();
        }
    }
}
