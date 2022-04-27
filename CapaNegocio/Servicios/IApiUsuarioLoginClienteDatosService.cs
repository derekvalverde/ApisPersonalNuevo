using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiUsuarioLoginClienteDatosService
    {
        clsUsuarioLoginClienteDatosResponse Authenticate3(string usuLogin);
    }
}
