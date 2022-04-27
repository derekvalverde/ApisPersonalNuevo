using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCondicionCamposAjustarService
    {
        List<clsCondicionCamposAjustarResponse> obtenerCamposAjustar();
        List<clsMaterialPrecio> verDescuento(string acu);
    }
}
