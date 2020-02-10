using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    abstract class Prestamo
    {
        protected double monto, pagoMensual, intereses, impuestos, capital, insoluto, totalPrestamo = 0;
        protected int plazo;
        protected decimal tasaAnual, tasaMensual, IVA;
        protected Boolean primerPago = true;


        private void establecerMonto(double monto)
        {
            this.monto = monto;
        }

        public void establecerPlazo(int plazo)
        {
            this.plazo = plazo;
        }

        public decimal calcularTasaMensual()
        {
            tasaMensual = (tasaAnual / 100) / 12;
            return tasaMensual;
        }

        public void establecerIVA(decimal IVA)
        {
            this.IVA = IVA;
        }

        public abstract double calcularPagoMensual();

        public double calcularIntereses()
        {
            if (primerPago == true)
            {
                intereses = (double)tasaMensual * monto;
                primerPago = false;
            }
            else
            {
                intereses = (double)tasaMensual * insoluto;
            }

            return intereses;
        }

        public double calcularImpuestos()
        {
            impuestos = intereses * (double) IVA;
            return impuestos;
        }

        public double calcularCapital()
        {
            capital = pagoMensual - (intereses + impuestos);
            return capital;
        }

        public double calcularSaldoInsoluto()
        {
            if (primerPago == true)
            {
                insoluto = monto - capital;
                primerPago = false;
            } else
            {
                insoluto = insoluto - capital;
            }
            
            return insoluto;
        }

        public double totalPagarPrestamo()
        {
            for(int i = 0; i<plazo; i++)
            {
                totalPrestamo = totalPrestamo + calcularPagoMensual();
            }
            return totalPrestamo;
        }
    }
}
