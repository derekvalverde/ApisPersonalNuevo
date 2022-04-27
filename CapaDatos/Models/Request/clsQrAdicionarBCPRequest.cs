using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsQrAdicionarBCPRequest
    {
        public string qrIdGenerado { get; set; }
        public string correlation { get; set; }
        public string moneda { get; set; }
        public decimal importe { get; set; }
        public string glosa { get; set; }
        public string serviceCode { get; set; }
        public string bussinesCode { get; set; }
        public DateTime qrFecha { get; set; }
        public List<clsQrAdicionarDetalleBCPRequest> detalleSolicitudAdicionar { get; set; }

    }
}
