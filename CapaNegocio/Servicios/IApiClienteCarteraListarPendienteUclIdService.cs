using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiClienteCarteraListarPendienteUclIdService
    {
        List<clsClienteCarteraListarPendienteUclIdResponse> obtenerPendientes(int uclId);
    }
}
