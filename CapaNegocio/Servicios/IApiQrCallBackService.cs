using CapaDatos.Response;

using System.Threading.Tasks;
using WebApiOutBCP.Models;

namespace CapaNegocio.Servicios
{
    public interface  IApiQrCallBackService
    {
        Task<clsQrCallBackResponse> verificarPago(clsQrCallBackRequest model);
    }
}
