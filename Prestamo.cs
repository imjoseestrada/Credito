using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    abstract class Prestamo
    {
        protected double monto, tasaAnual, tasaMensual, mensualidad, IVA = 0.16, primerInteres = 0, insoluto = 0;
        protected int plazo;
        protected bool primerCalculoIntereses = true, primerSaldoInsoluto = true;

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

        public double tasaMensualconIVA()
        {
            return (calcularTasaMensual() + (calcularTasaMensual() * IVA));
        }

        public double PagoMensual()
        {
            double denominador = Math.Pow((1 + tasaMensualconIVA()), plazo) - 1;

            mensualidad = (tasaMensualconIVA() + (tasaMensualconIVA() / denominador)) * monto;
            return mensualidad;
        }

        public double calcularTotalPrestamo()
        {
            double totalPrestamo = 0;
            for (int i = 0; i < plazo; i++)
            {
                totalPrestamo += mensualidad;
            }
            return totalPrestamo;
        }

        public double obtenerPagoMensual()
        {
            return Math.Round(PagoMensual(), 2);
        }

        public double obtenerTotalPrestamo()
        {
            return Math.Round(calcularTotalPrestamo(), 2);
        }

        public abstract double obtenerTasaAnual();

        public double Intereses()
        {
            double intereses = 0;
            if (primerCalculoIntereses == true)
            {
                intereses = tasaMensual * monto;
                primerInteres = intereses;
                // Aisla el importe del primer cálculo de intereses
                // para reutilizarlo más tarde al calcular
                // impuestos y capital
                primerCalculoIntereses = false;
            }
            else
            {
                intereses = tasaMensual * insoluto;
                primerInteres = 0;
            }

            return intereses;
        }

        public double SaldoInsoluto()
        {
            if (primerSaldoInsoluto == true)
            {
                insoluto = monto - Capital();
                primerSaldoInsoluto = false;
            }
            else
            {
                insoluto -= Capital();
            }

            return insoluto;
        }

        public double Impuestos()
        {
            double impuestos = 0;
            if(primerInteres != 0)
            {
                impuestos = primerInteres * IVA;
                // Si utilizo el método para calcular intereses
                // de manera directa, obtendría 0 porque para el
                // método Intereses() dejaría de ser el primer
                // calculo e insoluto no tiene valor asignado.
            }
            else
            {
                impuestos = Intereses() * IVA;
            }

            return impuestos;
        }

        public double Capital()
        {
            double capital = 0;
            if (primerInteres != 0)
            {
                capital = mensualidad - primerInteres - Impuestos();
                // Si utilizo el método para calcular intereses
                // de manera directa, obtendría 0 porque para el
                // método Intereses() dejaría de ser el primer
                // calculo e insoluto no tiene valor asignado.
            }
            else
            {
                capital = mensualidad - Intereses() - Impuestos();
            }
            return capital;
        }

        public string obtenerAmortizaciones()
        {
            String amortizaciones = "No. | Mensualidad | Intereses | Impuestos | Capital | Insoluto";
            amortizaciones += Environment.NewLine
                + "-------------------------------------------------------------";
            for (int i=0; i<plazo; i++)
            {
                amortizaciones += Environment.NewLine + (i+1)
                    + "     |    $" + Math.Round(mensualidad, 2)
                    + "     |    $" + Math.Round(Intereses(), 2)
                    + "     |    $" + Math.Round(Impuestos(), 2)
                    + "     |    $" + Math.Round(Capital(), 2)
                    + "     |    $" + Math.Round(SaldoInsoluto(), 2);
            }
            return amortizaciones;
        }
    }
}
