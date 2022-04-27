using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsPreguntaListarIdResponse
    {
        public int preId { get; set; }
        public string prePregunta { get; set; }
        public string mtiTipo { get; set; }
        public string mtiDetalle { get; set; }
        public List<clsRespuestaListarIdResponse> respuestaListarId { get; set; }
    }
}
