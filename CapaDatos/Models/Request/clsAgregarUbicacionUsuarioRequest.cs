using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsAgregarUbicacionUsuarioRequest
    {
        public string EmpCodigo { get; set; }
        public string EmuZona { get; set; }
        public string EmuDireccion { get; set; }
        public string EmuLat { get; set; }
        public string EmuLong { get; set; }
        public string EmpCodigoIni { get; set; }
    }
}
