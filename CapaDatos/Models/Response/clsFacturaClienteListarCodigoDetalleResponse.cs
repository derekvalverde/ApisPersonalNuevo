using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsFacturaClienteListarCodigoDetalleResponse
    {
        public string matCodigo { get; set; }
        public decimal fadCantidad { get; set; }
        public decimal fadPrecio { get; set; }
        public decimal fadCheque { get; set; }
    }
}
