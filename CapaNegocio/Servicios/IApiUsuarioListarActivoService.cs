using System.Collections.Generic;

using CapaDatos.Response;

namespace CapaNegocio.Servicios
{
    public interface IApiUsuarioListarActivoService
    {
         IEnumerable<ClsUsuarioListarActivoResponse> UsuarioListarActivo();
    }
}