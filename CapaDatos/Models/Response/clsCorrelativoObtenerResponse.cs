using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsCorrelativoObtenerResponse
    {
      
        public int corId { get; set; }
        public int corPedido { get; set; }
        public int corRecibo { get; set; }
        public int corReciboManual { get; set; }
        public int corReciboManualFinal { get; set; }
    }
}
