using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsEvaluacionUsuarioPreguntasListarRequest
    {
        public int cuuId { get; set; }
        public int evlId { get; set; }
        
        public string evuEstadoIntento { get; set; }
        public List<clsRespuestaAdicionarRequest> respuestaAdicionar { get; set; }

    }
}
