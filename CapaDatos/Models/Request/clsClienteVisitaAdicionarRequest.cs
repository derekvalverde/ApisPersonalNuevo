using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsClienteVisitaAdicionarRequest
    {
        public int usuId { get; set; }
        public int penId { get; set; }
        public string cliCodigo { get; set; }
        public float clgLatitud { get; set; }
        public float clgLongitud { get; set; }
        public DateTime clgFechaGPS { get; set; }
    }
}
