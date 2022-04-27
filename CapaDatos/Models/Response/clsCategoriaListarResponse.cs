using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsCategoriaListarResponse
    {
        public int catId { get; set; }
        public  string catNombre { get; set; }
        public string catColor { get; set; }
      
        public  DateTime catFecha { get; set; }
        public string catImagenDireccion { get; set; }
    }
}
