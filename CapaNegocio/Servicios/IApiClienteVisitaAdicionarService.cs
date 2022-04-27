using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiClienteVisitaAdicionarService
    {
        clsClienteVisitaAdicionarResponse obtenerVisitaAdicionar(int usuId, int penId, string cliCodigo, float clgLatitud, float clgLongitud, DateTime clgFechaGPS);
    }
}
