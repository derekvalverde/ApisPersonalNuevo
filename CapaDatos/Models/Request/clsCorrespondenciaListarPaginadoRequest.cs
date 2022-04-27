using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Request
{
    public class clsCorrespondenciaListarPaginadoRequest
    {
        public string UsuCodigo { get; set; }
        public int corNumPag { get; set; }
        public int corCantReg { get; set; }

    }
}
