using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsCursoUsuarioInscritoBusquedaListarRequest
    {
        public int ageId { get; set; }
        public int cargoId { get; set; }
        public string like { get; set; }
        public int curId { get; set; }
    }
}
