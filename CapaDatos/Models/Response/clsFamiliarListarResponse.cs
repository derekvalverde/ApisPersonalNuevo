using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsFamiliarListarResponse
    {
        public int famId { get; set; }
        public string famNombre { get; set; }
        public string famCarnet { get; set; }
        public DateTime famNacimiento { get; set; }
        public string famSexo { get; set; }
        public string famGradoActual { get; set; }
        public int famCursoActual { get; set; }
        public string fatNombre { get; set; }                
       
        public string cexNombre { get; set; }       
       
       // public string famEstado { get; set; }
       

    }
}
