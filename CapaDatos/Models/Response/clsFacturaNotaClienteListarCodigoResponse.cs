using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsFacturaNotaClienteListarCodigoResponse
    {
        public string cliCodigo { get; set; }
        public string cdfCodigo { get; set; }
        public string facCodigo { get; set; }
        public int facNumero { get; set; }
        public decimal clcMontoPago { get; set; }
        public DateTime clcFechaBase { get; set; }
        public string facEmpresa { get; set; }

    }
}
