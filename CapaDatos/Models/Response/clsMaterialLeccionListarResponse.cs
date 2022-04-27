using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsMaterialLeccionListarResponse
    {
        public int malId { get; set; }
        public string malNombre { get; set; }
        public string malDireccion { get; set; }
        public DateTime malFecha { get; set; }
        public int mtiId { get; set; }
    }
}
