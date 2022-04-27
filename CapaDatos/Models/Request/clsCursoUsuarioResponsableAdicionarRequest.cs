using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsCursoUsuarioResponsableAdicionarRequest
    {
        public int usuId { get; set; }
        public int curId { get; set; }
        public string curEstado { get; set; }
    }
}
