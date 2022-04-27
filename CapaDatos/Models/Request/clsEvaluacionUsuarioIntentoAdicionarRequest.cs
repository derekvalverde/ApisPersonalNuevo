using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
   public class clsEvaluacionUsuarioIntentoAdicionarRequest
    {
        public int cuuId { get; set; }
        public int evlId { get; set; }
        public int usuId { get; set; }
        public string evuEstadoIntento { get; set; }
    }
}
