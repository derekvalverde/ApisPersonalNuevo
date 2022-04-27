
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public interface IApiActivoUbicacionEnviarService
    {
        clsActivoUbicacionEnviarResponse obtenerUbicacion(string actCodigo, string uclImei);
            

    }
}
