using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    class Hipotecario: Prestamo
    {
        public override double obtenerTasaAnual()
        {
            return tasaAnual = 9;
        }
    }
}
