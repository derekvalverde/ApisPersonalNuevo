using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;


namespace CapaNegocio.Servicios
{
    public interface IApiQRGeneratedService
    {
        clsQrGeneratedResponse generarQR(Decimal monto, string CorrelationId, List<clsCollector> listaPedidosAPagar, List<clsQrAdicionarDetalleBCPRequest> detalleSolicitudAdicionar);
    }
}
