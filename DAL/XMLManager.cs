using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

namespace DAL
{
    public class XMLManager
    {
        private static string GetProjectRoot()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            while (dir != null && !Directory.Exists(Path.Combine(dir, "Ajedrez")))
            {
                dir = Directory.GetParent(dir)?.FullName;
            }

            return dir;
        }

        private static readonly string projectRoot = GetProjectRoot();

        private static readonly string PathHistorialXsd =
            Path.Combine(projectRoot, @"DAL\data\historial_partidas.xsd");

        private static readonly string PathHistorialXml =
            Path.Combine(projectRoot, @"DAL\data\historial_partidas.xml");


        public void GuardarPartidaEnHistorial(GameHistory gameHistory)
        {
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(PathHistorialXsd);

            if (File.Exists(PathHistorialXml))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, PathHistorialXsd);
                settings.ValidationType = ValidationType.Schema;

                using (XmlReader reader = XmlReader.Create(PathHistorialXml, settings))
                {
                    ds.ReadXml(reader);
                }
            }

            DataTable tPartida = ds.Tables["Partida"];
            DataRow rPartida = tPartida.NewRow();
            tPartida.Rows.Add(rPartida);

            DataTable tMeta = ds.Tables["Metadata"];
            DataRow rMeta = tMeta.NewRow();

            rMeta["Fecha"] = gameHistory.Fecha.ToString("s");
            rMeta["IdPartida"] = gameHistory.IdPartida;
            rMeta["IdJugadorBlancas"] = gameHistory.IdBlancas;
            rMeta["IdJugadorNegras"] = gameHistory.IdNegras;
            rMeta["IdGanador"] = gameHistory.IdGanador;
            rMeta["IdPerdedor"] = gameHistory.IdPerdedor;
            rMeta["Empate"] = gameHistory.Empate;
            rMeta["DuracionSegundos"] = gameHistory.DuracionSegundos;

            rMeta["Partida_Id"] = rPartida["Partida_Id"];

            tMeta.Rows.Add(rMeta);

            DataTable tMovimientos = ds.Tables["Movimientos"];
            DataRow rMovimientos = tMovimientos.NewRow();
            rMovimientos["Partida_Id"] = rPartida["Partida_Id"];
            tMovimientos.Rows.Add(rMovimientos);

            DataTable tMov = ds.Tables["Movimiento"];

            foreach (var mov in gameHistory.Movimientos)
            {
                DataRow r = tMov.NewRow();
                r["Movimiento_Column"] = mov;
                r["Movimientos_Id"] = rMovimientos["Movimientos_Id"];
                tMov.Rows.Add(r);
            }

            ds.WriteXml(PathHistorialXml);
        }

        public List<GameHistory> LeerHistorialConSchema()
        {
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(PathHistorialXsd);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, PathHistorialXsd);
            settings.ValidationType = ValidationType.Schema;

            using (XmlReader reader = XmlReader.Create(PathHistorialXml, settings))
            {
                ds.ReadXml(reader);
            }

            List<GameHistory> historial = new List<GameHistory>();

            DataTable tPartida = ds.Tables["Partida"];

            foreach (DataRow rPartida in tPartida.Rows)
            {
                GameHistory gh = new GameHistory();
                gh.Movimientos = new List<string>();

                foreach (DataRow rMeta in rPartida.GetChildRows("Partida_Metadata"))
                {
                    gh.Fecha = DateTime.Parse(rMeta["Fecha"].ToString());
                    gh.IdPartida = Convert.ToInt32(rMeta["IdPartida"]);
                    gh.IdBlancas = Convert.ToInt32(rMeta["IdJugadorBlancas"]);
                    gh.IdNegras = Convert.ToInt32(rMeta["IdJugadorNegras"]);
                    gh.IdGanador = Convert.ToInt32(rMeta["IdGanador"]);
                    gh.IdPerdedor = Convert.ToInt32(rMeta["IdPerdedor"]);
                    gh.Empate = Convert.ToBoolean(rMeta["Empate"]);
                    gh.DuracionSegundos = Convert.ToInt32(rMeta["DuracionSegundos"]);
                }

                foreach (DataRow rMovimientos in rPartida.GetChildRows("Partida_Movimientos"))
                {
                    foreach (DataRow rMov in rMovimientos.GetChildRows("Movimientos_Movimiento"))
                    {
                        gh.Movimientos.Add(rMov["Movimiento_Column"].ToString());
                    }
                }

                historial.Add(gh);
            }

            return historial;
        }
    }
}
