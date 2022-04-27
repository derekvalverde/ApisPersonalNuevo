using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsClienteCarteraPendienteResponse
    {
        public int clcId { get; set; }
        public string cliCodigo { get; set; }
        public string cdfCodigo { get; set; }
        public string clcDocumento { get; set; }
        public int clcPosicion { get; set; }
        
        public string facCodigo { get; set; }
        public string facOrigen { get; set; }
        public string clcReferencia { get; set; }
        
       
        public DateTime clcFechaContabilizacion { get; set; }
        public DateTime clcFechaBase { get; set; }
        public int clcTiempoMora { get; set; }
        public DateTime clcFechaPago { get; set; }
       
        public decimal clcMonto { get; set; }
       
        public string clcEstado { get; set; }
        public int clcCam { get; set; }
        
    }
}
