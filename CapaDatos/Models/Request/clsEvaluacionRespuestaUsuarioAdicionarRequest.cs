using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsEvaluacionRespuestaUsuarioAdicionarRequest
    {
        public int resId { get; set; }
        public int evuId { get; set; }
        public string eruEstado { get; set; }
    }
}
