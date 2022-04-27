using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsEmpleadoUbicacionListarResponse
    {
        public int emuId { get; set; }
        public string emuZona{ get; set; }
        public string emuDireccion { get; set; }
        public decimal emuLatitud{ get; set; }
        public decimal emuLongitud { get; set; }
    }
}
