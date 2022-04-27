using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsPreguntaAdicionarRequest
    {
        public int evaId { get; set; }
        public string prePregunta { get; set; }
        public string preEstado { get; set; }        
        public int mtiId { get; set; }
        public List<clsRespuestaAdicionarRequest> respuestaAdicionar { get; set; }
    }
}
