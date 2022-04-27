using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsEvaluacionModificarRequest
    {
        public int evaId { get; set; }
        public int modId { get; set; }
        public int tieId { get; set; }
        public string evaTitulo { get; set; }
    }
}
