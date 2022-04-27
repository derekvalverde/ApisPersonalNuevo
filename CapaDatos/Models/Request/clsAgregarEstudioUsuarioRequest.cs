using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsAgregarEstudioUsuarioRequest
    {

        public string EmpCodigo { get; set; }
        public string EstInstitucion { get; set; }
        public DateTime EstFecha { get; set; }
        public DateTime EstFechaFin { get; set; }
        public string EstExplicacion { get; set; }
        public string EstNombre { get; set; }
        public string EstNivel { get; set; }
        public int EttId { get; set; }
        public string EmpCodigoIni { get; set; }
    }
}
