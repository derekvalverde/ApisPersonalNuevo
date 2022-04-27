using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsPedidoSolicitudUclIddetalleResponse
    {
        
        public int pedId { get; set; }
        public string matCodigo { get; set; }
        public decimal pddCantidad { get; set; }
        public decimal pddCantidadAtendida { get; set; }
        
    }
}
