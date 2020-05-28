using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoEstacionamientos.clases
{
    class Validadores
    {
        public static bool ValidaNoVacio(string str)
        {
            return str.Length == 0;
        }

        public static bool ValidaLongitud(string str, int length)
        {
            return str.Length < length;
        }
        public static bool ValidaNumero(string text)
        {
            int sum = 0;
            try
            {
                sum = Convert.ToInt32(text);
            }
            catch (FormatException)
            {
            }
            return sum > 0;
        }
    }
}
