using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsExperienciaListarResponse
    {
  
        public string expInstitucion { get; set; }
        public string expCargo { get; set; }
        public DateTime expFecha { get; set; }
        public DateTime expFechaFin { get; set; }
        public string expMotivoRet { get; set; }
       
    }
}
