using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiUsuarioReseteoPasswordService
    {
        clsUsuarioReseteoPasswordResponse obtenerNuevoPassword(int usuId, string usuPassword, string usuPasswordNuevo, string aplicacion);
    }
}
