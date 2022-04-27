using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsDeudaCompensacionListarReciboDetalleResponse
    {

        public decimal dedMonto { get; set; }
        public int facNumero { get; set; }
        public string facOrigen { get; set; }
    }
}
