using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsCorrespondenciaDetalleListarCelularResponse
    {
        public int cdeId { get; set; }
        public string cdeCodigo { get; set; }
        public string corRemitente { get; set; }
        public string cdeDetalles { get; set; }
        public DateTime cdeFechaIni { get; set; }
        public string cdeEstado { get; set; }
    }
}
