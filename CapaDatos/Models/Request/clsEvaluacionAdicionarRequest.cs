using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
   public class clsEvaluacionAdicionarRequest
    {
        public int modId { get; set; }
        public int tieId { get; set; }
        public string evaTitulo { get; set; }
        public string evaEstado { get; set; }
        public DateTime evaFecha { get; set; }
        public int usuId { get; set; }
        public List<clsMaterialEvaluacionAdicionarRequest> materialAdicionar { get; set; }
    }
}
