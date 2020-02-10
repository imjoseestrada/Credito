using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    class Personal: Prestamo
    {
        public override double calcularPagoMensual()
        {
            double denominador = Math.Pow((1 + ((double) tasaMensual * (double) IVA)), plazo) - 1;

            pagoMensual = ((double) tasaMensual * (double) IVA) + (((double)tasaMensual * (double)IVA) / denominador) * monto;
            return pagoMensual;
        }
    }
}
