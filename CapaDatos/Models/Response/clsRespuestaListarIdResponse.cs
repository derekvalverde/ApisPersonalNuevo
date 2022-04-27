using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsRespuestaListarIdResponse
    {
        public int resId { get; set; }
        public string resRespuesta { get; set; }
        public byte resEsCorrecta { get; set; }
        public string mtiTipo { get; set; }
        public string mtiDetalle { get; set; }

    }
}
