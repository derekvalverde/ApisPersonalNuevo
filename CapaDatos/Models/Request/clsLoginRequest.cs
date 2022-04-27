using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsLoginRequest
    {
        public string Usuario { get; set; }
        public string Contra { get; set; }
        public string Imei { get; set; }
        public int GrpId { get; set; }
    }
}
