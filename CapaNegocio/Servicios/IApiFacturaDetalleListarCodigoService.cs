using CapaDatos.Request;
using System.Collections.Generic;


namespace CapaNegocio.Servicios
{
    public interface IApiFacturaDetalleListarCodigoService
    {
         IEnumerable<ClsFacturaDetalleListarCodigoResponse> Listar(string facCodigo);
    }
}