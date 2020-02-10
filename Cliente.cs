using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credito
{
    class Cliente
    {
        protected string idCliente;
        protected string idCuenta;
        protected string nombreCliente;

        public void establecerClienteID(String idCliente)
        {
            this.idCliente = idCliente;
        }

        public void establecerCuentaID(String idCuenta)
        {
            this.idCuenta = idCuenta;
        }

        public void establecerNombreCliente(String nombreCliente)
        {
            this.nombreCliente = nombreCliente;
        }

        public string obtenerClienteID()
        {
            return idCliente;
        }

        public string obtenerCuentaID()
        {
            return idCuenta;
        }

        public string obtenerNombreCliente()
        {
            return nombreCliente;
        }
    }
}
