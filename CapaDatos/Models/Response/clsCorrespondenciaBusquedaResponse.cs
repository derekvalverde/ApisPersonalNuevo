using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Response
{
    public class clsCorrespondenciaBusquedaResponse
    {
        public string corCodigo { get; set; }
        public string corNumGuia { get; set; }
        public string corRemitente { get; set; }
        public string codNombre { get; set; }
        public string areCodigo { get; set; }
        public DateTime corFechaIni { get; set; }
        public int nroDiv { get; set; }
        public string corEstado { get; set; }
        public string urgente { get; set; }
    }
}
