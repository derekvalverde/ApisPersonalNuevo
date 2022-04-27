using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiClienteCompletoVentaCobranzaService
    {
        List<clsClienteCompletoVentaCobranzaResponse> obtenerCompletoCobranza(int usuId);
    }
}
