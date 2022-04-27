using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsModuloModificarRequest
    {
        public int modId { get; set; }
        public string modTitulo { get; set; }
        public string modDescripcion { get; set; }
        public string modImagenDireccion { get; set; }


    }
}
