using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        public override string ToString()
        {
            return OwnerName;
        }
    }
}
