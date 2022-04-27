using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsEstudioListarResponse
    {
        public int estId { get; set; }
        public string estInstitucion{ get; set; }

        public DateTime estFecha { get; set; }
        public DateTime estFechaFin { get; set; }
        public string estNombre { get; set; }
        public string esgNombre { get; set; }
      
    }
}
