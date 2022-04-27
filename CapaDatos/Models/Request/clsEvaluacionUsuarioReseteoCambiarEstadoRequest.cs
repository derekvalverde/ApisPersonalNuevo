using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
   public class clsEvaluacionUsuarioReseteoCambiarEstadoRequest
    {
        public int eurId { get; set; }
        public int usuId { get; set; }
        public int evlId { get; set; }
        public string eurEstado { get; set; }

    }
}
