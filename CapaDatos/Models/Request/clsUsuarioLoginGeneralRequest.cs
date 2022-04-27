using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Request
{
   public class clsUsuarioLoginGeneralRequest
    {
        public string usuLogin { get; set; }
        public string usuPassword { get; set; }
        public int grpId { get; set; }
    }
}
