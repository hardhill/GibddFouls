using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class VFoul
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdTypeFoul { get; set; }
        public string TypeFoul { get; set; }
        public int IdReg { get; set; }
        public string Number { get; set; }
        public string Owner { get; set; }
        public override string ToString()
        {
            return $"{Date.ToString("dd-MM-yyyy")}\t{Number}\t{Owner}\t{TypeFoul}";
        }
    }
}
