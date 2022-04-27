using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsDeudaCompensacionVerificarFaltanteRequest
    {
        public int usuId { get; set; }
        public int recIdIni { get; set; }
        public int recIdFin { get; set; }
    }
}
