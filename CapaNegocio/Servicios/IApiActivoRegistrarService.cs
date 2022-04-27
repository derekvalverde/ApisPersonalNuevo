
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public interface IApiActivoRegistrarService
    {
        ClsActivoRegistrarResponse Registrar(string actCodigo, string actDenominacion, int empId, string usuCodigo);
    }

}
