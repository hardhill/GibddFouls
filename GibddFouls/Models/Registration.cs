using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class Registration
    {
        public int OwnerId { get; set; }
        public int CarId { get; set; }
        public string Number { get; set; }
        public override string ToString()
        {
            return $"{OwnerId}\t{CarId}\t{Number}";
        }
    }
}
