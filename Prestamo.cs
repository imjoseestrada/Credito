using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    abstract class Prestamo
    {
        protected double monto, denominador, pagoMensual, intereses, impuestos, capital, insoluto, totalPrestamo = 0;
        protected int plazo;
        protected double tasaMensual, tasaMensualConIVA;
        protected double tasaAnual;
        protected double IVA = 1.16;
        protected bool primerPago = true;


        public void establecerMonto(double monto)
        {
            this.monto = monto;
        }

        public void establecerPlazo(bool plazoMensual, int plazo)
        {
            if (plazoMensual == true)
            {
                this.plazo = plazo;
            } else
            {
                this.plazo = plazo * 12;
            }
        }

        public void establecerTasaAnual(double tasaAnual)
        {
            this.tasaAnual = tasaAnual;
        }

        public double calcularTasaMensual()
        {
            tasaMensual = (tasaAnual / 100) / 12;
            return tasaMensual;
        }

        public double calcularPagoMensual()
        {
            tasaMensualconIVA();
            denominador = Math.Pow((1 + tasaMensualConIVA), plazo) - 1;

            pagoMensual = (tasaMensualConIVA + (tasaMensualConIVA / denominador)) * monto;
            return pagoMensual;
        }

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
            impuestos = intereses * IVA;
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

        public double obtenerPagoMensual()
        {
            return Math.Round(pagoMensual, 2);
        }

        public double obtenerTotalPrestamo()
        {
            return Math.Round(totalPrestamo, 2);
        }

        public abstract double obtenerTasaAnual();

        public double tasaMensualconIVA()
        {
            return tasaMensualConIVA = tasaMensual * IVA;
        }
    }
}
