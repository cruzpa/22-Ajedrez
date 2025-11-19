using System;

namespace BE
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public JugadorHistorial Historial { get; set; }

        public Jugador() {}   
        public Jugador(String Name, String Pass)
        {
            this.Name = Name;
            this.Pass = Pass;
        }
    }
}