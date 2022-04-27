using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsCorrespondenciaDetalleListarResponse
    {

            public int cdeId { get; set; }
            public string cdeCodigo { get; set; }
            public string corRemitente { get; set; }

            public string cdeDetalles { get; set; }
            public string corCodigo { get; set; }
           
            public DateTime cdeFechaIni { get; set; }
            public string cdeEstado { get; set; }

            public string destinatario { get; set; }
            public string usuActual { get; set; }
            public string couActual { get; set; }
    }
}
