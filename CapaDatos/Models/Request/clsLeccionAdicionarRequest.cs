using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsLeccionAdicionarRequest
    {
        public int modId { get; set; }
        public string lecTitulo { get; set; }
        public string lecEstado { get; set; }
        public int usuId { get; set; }
        public List<clsMaterialLeccionAdicionarRequest> materialAdicionar { get; set; }

    }
}
