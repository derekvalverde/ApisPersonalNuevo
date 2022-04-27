using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsUsuarioLoginClienteGrallRequest
    {
        public string usuLogin { get; set; }
        public string usuPassword { get; set; }
        public string usuImei { get; set; }
        public int  grpId { get; set; }
    }
}
