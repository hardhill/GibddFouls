using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Models
{
    public class FoulsRep
    {
        public FoulsRep()
        {
            Data = new List<OnePoint>();
        }
        public class OnePoint
        {
            public OnePoint(DateTime dt,int value)
            {
                xdata = dt;
                Value = value;
            }
            public DateTime xdata { get; set; }
            public double ddata { get { 
                    return Convert.ToDouble(xdata.Ticks); 
                } 
            }
            public int Value { get; set; }
            
        }
        public List<OnePoint> Data { get; set; }
        public void AddData(DateTime dt,int value)
        {
            Data.Add(new OnePoint(dt, value));
        }
    }
}
