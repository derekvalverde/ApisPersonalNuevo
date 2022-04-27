using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
   public class clsEvaluacionUsuarioIntentoAdicionarResponse
    {
        public int ID { get; set; }
        public int preId { get; set; }
        public int evlId { get; set; }
        public string prePregunta { get; set; }
        public int evaId { get; set; }
        public string mtiTipo { get; set; }
        public string mtiDetalle { get; set; }

        public List<clsRespuestaListarIdResponse> respuestaListarId { get; set; }
    }
}
