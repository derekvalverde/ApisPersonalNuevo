using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsChequeListarPendienteResponse
    {
        public int cheId { get; set; }
        public string choDetalle { get; set; }
        public int chdId { get; set; }
        public decimal cheMonto { get; set; }
        public DateTime cheFecha { get; set; }
        public string cliCodigo { get; set; }
    }
}
