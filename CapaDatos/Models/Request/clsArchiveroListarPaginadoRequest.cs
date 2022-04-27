using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Request
{
    public class clsArchiveroListarPaginadoRequest
    {
        public string UsuCodigo { get; set; }
        public int arcNumPag { get; set; }
        public int arcCantReg { get; set; }
    }
}
