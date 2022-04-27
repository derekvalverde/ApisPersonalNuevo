using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CapaDatos.Request
{
    public class clsPedidoSolicitudModifcarEstadoRequest
    {
        public int pedId { get; set; }
        public string pedEstado { get; set; }
        public int usuId { get; set; }
    }
}
