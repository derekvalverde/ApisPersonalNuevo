using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsCambioRegistroRequest
    {
        public int camId { get; set; }
        public int usuId { get; set; }
        public string camTabla { get; set; }
        public string camTipo { get; set; }
        public string camCampo { get; set; }
        public int camRegistroId { get; set; }
        public int camAgrupador { get; set; }

        public string camAntiguo { get; set; }
        public string camNuevo { get; set; }
        public string camEstado { get; set; }
        public int camUsuIdR { get; set; }

        public DateTime camFechaInicial { get; set; }
        public DateTime camFechaRevision { get; set; }





    }
}
