using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsDeudaCompensacionAdicionarCabeceraRequest
    {
        public int recId { get; set; }
        public int recManual { get; set; }
        public string cliCodigo { get; set; }
        public decimal decMonto { get; set; }
        public DateTime decFecha { get; set; }
        public int usuId { get; set; }
        
        public List<clsDeudaCompensacionadicionarDetalleRequest> detalleDeudaCompensacion { get; set; }
    }
}
