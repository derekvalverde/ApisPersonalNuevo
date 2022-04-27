using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsQrAdicionarDetalleBCPRequest
    {        
        public string correlation { get; set; }        
        public string sodNombre { get; set; }
        public string facNumero { get; set; }
        public decimal facMonto { get; set; }
       
    }
}
