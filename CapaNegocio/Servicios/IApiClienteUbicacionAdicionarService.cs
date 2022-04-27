using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiClienteUbicacionAdicionarService
    {
        clsClienteUbicacionAdicionaResponse obtenerUbicacionAdicionar(string cliCodigo, int usuId, float cluLatitud, float cluLongitud);
    }
}
