using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class Car
    {
        public int IdCar { get; set; }
        public string Carname { get; set; }
        public string Caryear { get; set; }

        public override string ToString()
        {
            return $"{Carname}\t[{Caryear}]";
        }
    }
}
