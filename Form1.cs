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
        bool plazoMensual, nuevo = false;
        Prestamo[] Prestamos = new Prestamo[1];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // limpiar campos
            txtNumeroCliente.Text = null;
            txtNumeroCuenta.Text = null;
            txtNombreCliente.Text = null;
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
            txtNumeroCliente.Enabled = true;
            txtNumeroCuenta.Enabled = true;
            txtNombreCliente.Enabled = true;

            txtNumeroCliente.Focus();
            nuevo = true;
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtNumeroCliente.Text != "" && txtNumeroCuenta.Text != "" && txtNombreCliente.Text != "")
            {
                Cliente objCliente = new Cliente();

                // enviar datos del cliente
                objCliente.establecerClienteID(txtNumeroCliente.Text);
                objCliente.establecerCuentaID(txtNumeroCuenta.Text);
                objCliente.establecerNombreCliente(txtNombreCliente.Text);

                // habilitar & deshabilitar
                txtNumeroCliente.Enabled = false;
                txtNumeroCuenta.Enabled = false;
                txtNombreCliente.Enabled = false;
                cbTipoCredito.Enabled = true;
                txtMonto.Enabled = true;
                txtPlazo.Enabled = true;
                rbAnual.Enabled = true;
                rbMensual.Enabled = true;
                btnCalcular.Enabled = true;
                btnValidar.Enabled = false;

                cbTipoCredito.Focus();
            }
            else
            {
                MessageBox.Show("Debes completar todos los datos del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                // información del credito
                Prestamos[0].establecerMonto(Convert.ToDouble(txtMonto.Text));
                Prestamos[0].establecerTasaAnual(Convert.ToDouble(txtTasaAnual.Text));
                Prestamos[0].establecerPlazo(plazoMensual, Convert.ToInt32(txtPlazo.Text));

                // cálculos del credito
                Prestamos[0].PagoMensual();
                Prestamos[0].calcularTotalPrestamo();

                // mostrar información del credito
                txtMensualidad.Text = Prestamos[0].obtenerPagoMensual().ToString();
                txtTotalPrestamo.Text = Prestamos[0].obtenerTotalPrestamo().ToString();
                txtAmortizaciones.Text = Prestamos[0].obtenerAmortizaciones();

                // habilitar & deshabilitar
                btnCalcular.Enabled = false;
                btnNuevo.Enabled = true;
                cbTipoCredito.Enabled = false;
                txtMonto.Enabled = false;
                txtPlazo.Enabled = false;
                rbAnual.Enabled = false;
                rbMensual.Enabled = false;
                txtAmortizaciones.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Debes completar todos los datos del préstamo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbTipoCredito_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string seleccion = cbTipoCredito.SelectedItem.ToString();

                switch (seleccion)
                {
                    case "Hipotecario":
                        Prestamos[0] = new Hipotecario();
                        break;
                    case "Automotriz":
                        Prestamos[0] = new Automotriz();
                        break;
                    case "Personal":
                        Prestamos[0] = new Personal();
                        break;
                }

                txtTasaAnual.Text = Prestamos[0].obtenerTasaAnual().ToString();
                txtMonto.Focus();
            }
            catch
            {
                if (nuevo == true)
                {
                    MessageBox.Show("Selecciona un tipo de crédito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbTipoCredito.SelectedItem = null;
                    cbTipoCredito.Text = "Seleccionar";
                    nuevo = false;
                }
            }
        }

        private void rbPlazo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAnual.Checked == true)
            {
                label10.Text = "años";
                plazoMensual = false;
                txtPlazo.Focus();
            } else if (rbMensual.Checked == true)
            {
                label10.Text = "meses";
                plazoMensual = true;
            }
            txtPlazo.Focus();
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
    }
}
