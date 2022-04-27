using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsUsuarioLoginGeneralCardRequest
    {
        public string usuLogin { get; set; }
        public string usuPassword { get; set; }  
        public int grpId { get; set; }
    }
}
