using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddFouls.Classes
{
    public class Util
    {
        public static string LoadSQL(string filename)
        {
            string result = "";
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception) { }
            
            return result;
        }
    }
}
