using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsRespuestaAdicionarRequest
    {
        public int preId { get; set; }
        public string resRespuesta { get; set; }
        public bool resEsCorrecta { get; set; }
        public string resEstado { get; set; }
        public DateTime resFecha { get; set; }
        public int mtiId { get; set; }
    }
}
