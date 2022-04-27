using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiClienteAdicionalAdicionarService
    {
        clsClienteAdicionalAdicionarResponse obtenerClienteAdicionar(string cliCodigo, string caaEmail);
    }
}
