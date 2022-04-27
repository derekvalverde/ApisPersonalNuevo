using CapaDatos.Models.Response;
using CapaDatos.Response;


namespace CapaNegocio.Servicios
{
    public interface IApiActivoLeerService
    {
        clsActivoLeerResponse activoLeer(string actCodigo, int usuId, int empId);
    }
}
