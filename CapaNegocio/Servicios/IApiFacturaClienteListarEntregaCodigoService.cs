using System.Collections.Generic;
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public interface IApiFacturaClienteListarEntregaCodigoService
    {
         IEnumerable<ClsFacturaClienteListarEntregaCodigoResponse> Listar(string cliCodigo);
    }
}