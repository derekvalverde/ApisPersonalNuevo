using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiClienteVisitaListarService
    {
        List<clsClienteVisitaListarResponse> obtenerClienteVisitaListar(int usuId, DateTime clgFecha, DateTime clgFechaGPS);
    }
}
