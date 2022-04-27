using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsMaterialEvaluacionAdicionarRequest
    {
        public int evaId { get; set; }
        public string maeNombre { get; set; }
        public string maeDireccion { get; set; }
        public string maeEstado { get; set; }
        public DateTime maeFecha { get; set; }
        public int mtiId { get; set; }

    }
}
