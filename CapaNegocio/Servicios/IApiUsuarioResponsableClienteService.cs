using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiUsuarioResponsableClienteService
    {
        List<clsUsuarioResponsableClienteResponse> obtenerUsuarioResponsableCliente(string cliCodigo);
    }
}
