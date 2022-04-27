using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsMaterialLeccionAdicionarRequest
    {
        public int lecId { get; set; }
        public string malNombre { get; set; }
        public string malDireccion { get; set; }              
        public int mtiId { get; set; }

    }
}
