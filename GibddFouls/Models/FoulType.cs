using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class FoulType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
