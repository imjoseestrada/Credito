using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    abstract class Prestamo
    {
        protected double monto, tasaAnual, tasaMensual, mensualidad, IVA = 0.16;
        protected double intereses, impuestos, capital, insoluto;
        protected double primerInteres = 0, primerImpuesto = 0, primerCapital = 0, primerInsoluto = 0;
        protected int plazo;

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
            if (primerInteres == 0)
            {
                intereses = tasaMensual * monto;
                primerInteres = intereses;
            }
            else
            {
                intereses = tasaMensual * insoluto;
            }

            return intereses;
        }

        public double Impuestos()
        {
            if (primerImpuesto == 0)
            {
                impuestos = primerInteres * IVA;
                primerImpuesto = impuestos;
            }
            else
            {
                impuestos = Intereses() * IVA;
            }

            return impuestos;
        }

        public double Capital()
        {
            if (primerCapital == 0)
            {
                capital = mensualidad - primerInteres - primerImpuesto;
                primerCapital = capital;
            }
            else
            {
                capital = mensualidad - Intereses() - Impuestos();
            }
            return capital;
        }

        public double SaldoInsoluto()
        {
            if (primerInsoluto == 0)
            {
                insoluto = monto - primerCapital;
                primerInsoluto = insoluto;
            }
            else
            {
                insoluto -= Capital();
            }

            return insoluto;
        }

        public string obtenerAmortizaciones()
        {
            double acumIntereses = 0, acumImpuestos = 0, acumCapital = 0;

            String amortizaciones = "No. | Mensualidad | Intereses | Impuestos | Capital | Insoluto";
            amortizaciones += Environment.NewLine
                + "-------------------------------------------------------------";
            for (int i = 0; i < plazo; i++)
            {
                intereses = Intereses(); impuestos = Impuestos(); capital = Capital();

                amortizaciones += Environment.NewLine + (i + 1)
                    + "     |    $" + Math.Round(mensualidad, 2)
                    + "     |    $" + Math.Round(intereses, 2)
                    + "     |    $" + Math.Round(impuestos, 2)
                    + "     |    $" + Math.Round(capital, 2)
                    + "     |    $" + Math.Round(SaldoInsoluto(), 2);

                acumIntereses += intereses;
                acumImpuestos += impuestos;
                acumCapital += capital;
            }

            amortizaciones += Environment.NewLine
                + "-------------------------------------------------------------";
            amortizaciones += Environment.NewLine + "Intereses: $" + Math.Round(acumIntereses, 2);
            amortizaciones += Environment.NewLine + "Impuestos: $" + Math.Round(acumImpuestos, 2);
            amortizaciones += Environment.NewLine + "Capital: $" + Math.Round(acumCapital, 2);

            return amortizaciones;
        }
    }
}
