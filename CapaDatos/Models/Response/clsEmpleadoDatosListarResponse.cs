using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsEmpleadoDatosListarResponse
    {
        public int empId { get; set; }
        public string empNombre { get; set; }
        public DateTime empNacimientoFecha { get; set; }
        public string empNacimientoLugar { get; set; }
        public string empSexo { get; set; }
        public string empConyugeNombre { get; set; }
        public string empConyugeCelPriv { get; set; }
        public string empCarnet { get; set; }
        public DateTime empCarnetCad { get; set; }           

        public int empLicencia { get; set; }
        public DateTime empLicenciaCad { get; set; }
       
        public DateTime empFechaIngreso { get; set; }
        public string empAfp { get; set; }
       
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
        public string empFacebook { get; set; }
        public string empHobby { get; set; }


        public string empEstado { get; set; }

        public string areNombre { get; set; }       
        public string ofiNombre { get; set; }
        public string cecNombre { get; set; }
        public string carNombre { get; set; }
        public string cexNombre { get; set; }
        public string eecNombre { get; set; }

        /* public DateTime empFechaIni { get; set; }
         public DateTime empFechaMod { get; set; }
         public DateTime empFechaFin { get; set; }
         public int empUsuIniId { get; set; }
         public int empUsuModId { get; set; }
         public int empUsuFinId { get; set; }*/


    }
}
