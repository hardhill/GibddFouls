using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class Foul
    {
        public DateTime DtFoul { get; set; }
        public int IdRegistr { get; set; }
        public int IdTypeFoul { get; set; }
    }
}
