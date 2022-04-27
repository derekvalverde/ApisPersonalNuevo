using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCorrespondenciaDetalleBusquedaService
    {
        List<clsCorrespondenciaDetalleBusquedaResponse> obtenerCorrespondenciaDetalleBusqueda(string UsuCodigo, string codigo, string detalle, string remitente, DateTime fechaIni, DateTime fechaFin);
    }
}
