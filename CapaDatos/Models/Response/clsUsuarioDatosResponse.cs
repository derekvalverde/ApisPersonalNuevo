using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsUsuarioDatosResponse
    {
        
        public string empCodigo{ get; set; }
        public string empNombre { get; set; }
        public DateTime empNacimientoFecha{ get; set; }
        public string empNacimientoLugar { get; set; }
        public string empSexo { get; set; }
        public int empCarnet { get; set; }
        public string empCarnetExp { get; set; }
        public DateTime empCarnetCaducado { get; set; }

        public int empLicencia { get; set; }
        public DateTime empLicenciaCad { get; set; }
        public string empAfp{ get; set; }
        public DateTime empFechaIngreso { get; set; }
        public string empNua { get; set; }
        public string empCaja { get; set; }
        public string empNumSalud { get; set; }

        public int empTelPriv { get; set; }
        public int empCelPriv { get; set; }
        public int empCelTrab { get; set; }
        public int empInterno { get; set; }
        public string empLinkEdin { get; set; }
        public string empEmailTrab { get; set; }
        public string empEmailPriv { get; set; }
        public string empEstadoCiv { get; set; }
        public string empCarNombre { get; set; }
        public string areNombre{ get; set; }
        public string cecNombre { get; set; }
        public string ofiNombre { get; set; }
        
       /* public DateTime empFechaIni { get; set; }
        public DateTime empFechaMod { get; set; }
        public DateTime empFechaFin { get; set; }
        public int empUsuIniId { get; set; }
        public int empUsuModId { get; set; }
        public int empUsuFinId { get; set; }*/
       


    }
}
