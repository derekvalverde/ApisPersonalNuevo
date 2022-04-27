using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsCursoListarResponse
    {

        public int curId { get; set; }
        public string curNombre { get; set; }

        public string curInstitucion { get; set; }
      
        public string curExplicacion  { get; set; }
        public DateTime curFecha { get; set; }
    }
}
