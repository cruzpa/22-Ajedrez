using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ajedrez
{
    public partial class HistorialPartidas : Form
    {
        XMLHelper xMLHelper = new XMLHelper();
        List<GameHistory> historial = new List<GameHistory>();
        JugadorBLL JugadorBLL = new JugadorBLL();

        public HistorialPartidas(Jugador jugador)
        {
            InitializeComponent();
            Enlazar(jugador);
        }

        public void Enlazar(Jugador jugador)
        {
            label1.Text = $"Historial de partidas de {jugador.Name}";

            historial = xMLHelper.LeerHistorial();

            var partidasJugador = historial
                .Where(p => p.IdBlancas == jugador.Id || p.IdNegras == jugador.Id)
                .ToList();

            var items = partidasJugador.Select(p => new
            {
                Partida = p,
                Texto = $"{p.IdPartida} --- Rival: {ObtenerRival(p, jugador)} --- {ObtenerResultado(p, jugador)} --- {p.Fecha.ToShortDateString()} "
            }).ToList();

            comboBox1.DisplayMember = "Texto";
            comboBox1.ValueMember = "Partida";
            comboBox1.DataSource = items;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            if (items.Any())
                ActualizarMovimientos(items.First().Partida);
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = comboBox1.SelectedItem;
            if (selected == null) return;

            var partida = (GameHistory)selected.GetType().GetProperty("Partida").GetValue(selected);

            ActualizarMovimientos(partida);
        }

        private void ActualizarMovimientos(GameHistory partida)
        {
            label2.Text = string.Join("   ", partida.Movimientos);
        }

        // ------------------------------------------------
        // Helpers
        // ------------------------------------------------

        private string ObtenerRival(GameHistory p, Jugador jugador)
        {

            if (p.IdBlancas == jugador.Id)
                return JugadorBLL.Leer(p.IdNegras);

            if (p.IdNegras == jugador.Id)
                return JugadorBLL.Leer(p.IdBlancas);

            return "Desconocido";
        }

        private string ObtenerResultado(GameHistory p, Jugador jugador)
        {
            bool gano = p.IdGanador == jugador.Id;
            bool perdio = p.IdPerdedor == jugador.Id;

            if (p.Empate)
                return "Empate";

            if (gano)
                return "Ganada";

            if (perdio)
                return "Perdida";

            return "N/A";
        }
    }
}
