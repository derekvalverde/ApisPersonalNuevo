using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsModuloAdicionarRequest
    {
        public int curId { get; set; }
        public string modTitulo { get; set; }
        public string modDescripcion { get; set; }
        public string modEstado { get; set; }
        public string modImagenDireccion { get; set; }
        public int usuId { get; set; }

    }
}
