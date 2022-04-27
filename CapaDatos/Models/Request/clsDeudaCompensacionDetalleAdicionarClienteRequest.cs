using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsDeudaCompensacionDetalleAdicionarClienteRequest
    {
       
        public int clcId { get; set; }
        public string facCodigo { get; set; }
        public string facOrigen { get; set; }
        public decimal dedMonto { get; set; }
        public string dedNumero { get; set; }
        public string banCodigo { get; set; }
    }
}
