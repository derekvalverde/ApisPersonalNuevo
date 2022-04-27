using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiPedidoSolicitudUclIdService
    {
        List<clsPedidoSolicitudUclIdCabeceraResponse> obtenerPedidoSol(int uclIdld, int usuId, string aplicacion);
    }
}
