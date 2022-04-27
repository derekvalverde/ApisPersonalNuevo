using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsMaterialLineaVentaStockBuscadorRequest
    {
        public string ageOficina { get; set; }
        public string linCodigo{ get; set; }
        public string like { get; set; }
        public string cliCodigo { get; set; }
        public string aplicacion { get; set; }
        public string matCodigo { get; set; }
        public int permiso { get; set; }

    }
}
