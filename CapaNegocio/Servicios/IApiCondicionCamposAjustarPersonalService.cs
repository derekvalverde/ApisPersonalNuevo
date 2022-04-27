using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCondicionCamposAjustarPersonalService
    {
        List<clsCondicionCamposAjustarPersonalResponse> obtenerCamposAjustarPersonal();
        List<clsMaterialPrecio> verDescuento(string acu);
    }
}
