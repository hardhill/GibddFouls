using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class VRegistration
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int CarId { get; set; }
        public string Carmodel { get; set; }
        public string Caryear { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public override string ToString()
        {
            return $"{Number}\t{Carmodel}\t{Caryear}\t{Owner}";
        }
    }
}
