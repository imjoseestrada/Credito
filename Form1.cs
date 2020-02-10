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
        public Form1()
        {
            InitializeComponent();
        }

        private void SoloLetras(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void NumerosDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // limpiar campos
            txtNumeroCliente.Text = null;
            txtNumeroCuenta.Text = null;
            txtNombreCliente.Text = null;
            cbTipoCredito.SelectedIndex = -1;
            txtCapital.Text = null;
            txtEnganche.Text = null;
            txtInteres.Text = null;
            txtPlazo.Text = null;
            txtMensualidad.Text = null;
            txtAmortizaciones.Text = null;

            // habilitar & deshabilitar
            btnNuevo.Enabled = false;

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            
        }
    }
}
