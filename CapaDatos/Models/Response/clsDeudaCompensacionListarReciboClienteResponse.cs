using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsDeudaCompensacionListarReciboClienteResponse
    {

        public int recId { get; set; }
        public int decId { get; set; }
        public decimal decMonto { get; set; }
        public DateTime decFecha { get; set; }
        public string Estado { get; set; }
       
    }
}
