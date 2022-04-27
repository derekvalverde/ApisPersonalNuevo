using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiMaterialLineaVentaStockService
    {
        List<clsMaterialLineaVentaStockResponse> obtenerMatLinea(string linCodigo, string ageOficina, int permiso, string aplicacion);
    }
}
