using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsLeccionModificarRequest
    {
        public int lecId { get; set; }
        public int modId { get; set; }
        public string lecTitulo { get; set; }
    }
}
