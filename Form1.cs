using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Credito
{
    public partial class Form1 : Form
    {
        bool plazoMensual;
        int plazo;
        double monto, tasaAnual;
        Cliente objCliente = new Cliente();
        Prestamo[] Prestamos = new Prestamo[1];

        public Form1()
        {
            InitializeComponent();
        }

        private void SoloLetras(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo puedes ingresar letras en este campo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo puedes ingresar números en este campo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // limpiar campos
            txtNumeroCliente.Text = null;
            txtNumeroCuenta.Text = null;
            txtNombreCliente.Text = null;
            cbTipoCredito.SelectedItem = "Personal";
            txtMonto.Text = null;
            txtTotalPrestamo.Text = null;
            txtTasaAnual.Text = null;
            txtPlazo.Text = null;
            txtMensualidad.Text = null;
            txtAmortizaciones.Text = null;
            rbAnual.Checked = false;
            rbMensual.Checked = false;

            // habilitar & deshabilitar
            btnNuevo.Enabled = false;
            btnValidar.Enabled = true;
            cbTipoCredito.Enabled = false;
            txtMonto.Enabled = false;
            txtPlazo.Enabled = false;
            rbAnual.Enabled = false;
            rbMensual.Enabled = false;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                monto = Convert.ToDouble(txtMonto.Text);
                tasaAnual = Convert.ToDouble(txtTasaAnual.Text);
                plazo = Convert.ToInt32(txtPlazo.Text);

                // informacion del credito
                Prestamos[0].establecerMonto(monto);
                Prestamos[0].establecerTasaAnual(tasaAnual);
                Prestamos[0].establecerPlazo(plazoMensual, plazo);

                // realizar cálculos del credito
                Prestamos[0].calcularTasaMensual();
                Prestamos[0].calcularPagoMensual();
                Prestamos[0].totalPagarPrestamo();

                // mostrar informacion del credito
                txtMensualidad.Text = Prestamos[0].obtenerPagoMensual().ToString();
                txtTotalPrestamo.Text = Prestamos[0].obtenerTotalPrestamo().ToString();

                btnCalcular.Enabled = false;
                btnNuevo.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Debes completar todos los datos del préstamo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtNumeroCliente.Text != "" && txtNumeroCuenta.Text != "" && txtNombreCliente.Text != "")
            {
                // habilitar campos
                cbTipoCredito.Enabled = true;
                txtMonto.Enabled = true;
                txtPlazo.Enabled = true;
                rbAnual.Enabled = true;
                rbMensual.Enabled = true;

                btnCalcular.Enabled = true;
                btnValidar.Enabled = false;

                // enviar datos del cliente
                objCliente.establecerClienteID(txtNumeroCliente.Text);
                objCliente.establecerCuentaID(txtNumeroCuenta.Text);
                objCliente.establecerNombreCliente(txtNombreCliente.Text);
            }
            else
            {
                MessageBox.Show("Debes completar todos los datos del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbAnual_CheckedChanged(object sender, EventArgs e)
        {
            label10.Text = "años";
            plazoMensual = false;
        }

        private void cbTipoCredito_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string seleccion = cbTipoCredito.SelectedItem.ToString();

                if (seleccion.Equals("Hipotecario"))
                {
                    Prestamos[0] = new Hipotecario();
                }
                if (seleccion.Equals("Automotriz"))
                {
                    Prestamos[0] = new Automotriz(); 
                }
                else
                {
                    Prestamos[0] = new Personal();
                }

                // obtener Tasa Anual
                txtTasaAnual.Text = Prestamos[0].obtenerTasaAnual().ToString();
            }
            catch
            {
                MessageBox.Show("Debes seleccionar un valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbMensual_CheckedChanged(object sender, EventArgs e)
        {
            label10.Text = "meses";
            plazoMensual = true;
        }
    }
}
