using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiConocimientoListarService
    {
        List<clcConocimientoListarResponse> obtenerUsuarioEstudios(int usuId);
    }
}
