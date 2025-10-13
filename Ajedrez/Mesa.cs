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
        public Tablero tablero = new Tablero(); 
        public Mesa()
        {
            InitializeComponent();
            tablero.EnviarCasillero += Tablero_EnviarCasillero;
        }

        private void Tablero_EnviarCasillero(Casillero casillero)
        {
            UI_CASILLERO ui_casillero = new UI_CASILLERO();
            ui_casillero.Location = new Point(
                (casillero.X) * casillero.Ancho + (10 * casillero.X),
                (casillero.Y) * casillero.Ancho + (10 * casillero.Y)
                );
            ui_casillero.Size = new Size(casillero.Ancho, casillero.Ancho);
            ui_casillero.Casillero = casillero;

            //`ui_casillero.EnviarCasillero += Cas_EnviarCasillero;

            this.Controls.Add(ui_casillero);
        }

        private void Mesa_Shown(object sender, EventArgs e)
        {
            tablero.InicializarTablero();
        }
    }
}
