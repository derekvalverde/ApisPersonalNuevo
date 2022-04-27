
using CapaDatos.Request;
using System.Collections.Generic;

namespace CapaNegocio.Servicios
{
    public interface IApiInventarioVerificarService
    {
        List<clsInventarioVerificarResponse> obtenerInventarioVerificar(string actCodigo);
    }
}
