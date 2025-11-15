using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez
{
    public class JugadorHistorial
    {
        public int Id { get; set; }
        public int Win { get; set; }
        public int Tie { get; set; }
        public int Loss { get; set; }
        public int TimePlayedSeconds { get; set; }
    }
}
