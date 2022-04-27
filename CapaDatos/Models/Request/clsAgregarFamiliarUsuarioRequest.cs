using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsAgregarFamiliarUsuarioRequest
    {
        public string EmpCodigo { get; set; }
        public string FamNombre { get; set; }
        public int FamCarnet { get; set; }
        public string FamCarnetExt { get; set; }

        public int FamCelular { get; set; }
        public string FamTipo { get; set; }
        public DateTime FamNacimiento { get; set; }
        public string FamSexo { get; set; }
        public string FamGradoActual { get; set; }
        public int FarmCursoActual { get; set; }
        public string EmpCodigoIni { get; set; }

    }
}
