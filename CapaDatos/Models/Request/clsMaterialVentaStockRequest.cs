using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsMaterialVentaStockRequest
    {
    
        public int uclId { get; set; }
        public string like { get; set; }
        public string cliCodigo { get; set; }
        public string ageOficina { get; set; }
        public int usuId { get; set; }
        public string aplicacion { get; set; }
        public int permiso { get; set; }
    }
}
