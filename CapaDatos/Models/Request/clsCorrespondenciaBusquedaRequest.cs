using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Request
{
    public class clsCorrespondenciaBusquedaRequest
    {
        public string UsuCodigo { get; set; }
        public string  corCodigo{ get; set; }
        public string corRemitente { get; set; }
        public string arecodigo { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }
    }
}
