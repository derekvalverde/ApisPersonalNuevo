using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsCorreoClienteAdicionarRequest
    {
        public int uclId { get; set; }
        public string coeAsunto { get; set; }
        public string coeDetalle { get; set; }

    }
}
