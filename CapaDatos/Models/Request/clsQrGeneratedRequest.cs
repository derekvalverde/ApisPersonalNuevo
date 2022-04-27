using CapaDatos.Models;
using CapaDatos.Request;
using System;
using System.Collections.Generic;


namespace CapaDatos.Request
{
    public class clsQrGeneratedRequest
    {
        public decimal Amount { get; set; }
        public String CorrelationId { get; set; }
        public List<clsCollector> listPedidosAPagar { get; set; }
        public List<clsQrAdicionarDetalleBCPRequest> detalleSolicitudAdicionar { get; set; }
    }
}
