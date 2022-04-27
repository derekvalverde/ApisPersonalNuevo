using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsClienteVisitaListarRequest
    {
        public int usuId { get; set; }
        public DateTime clgFecha { get; set; }
        public DateTime clgFechaGPS { get; set; }
    }
}
