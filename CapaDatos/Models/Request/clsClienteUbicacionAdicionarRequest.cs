using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsClienteUbicacionAdicionarRequest
    {
        public string cliCodigo { get; set; }
        public int  usuId { get; set; }
        public float cluLatitud  { get; set; }
	    public float cluLongitud { get; set; }
    }
}
