using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiMaterialLineaVentaStockBuscadorService
    {
        List<clsMaterialLineaVentaStockBuscadorResponce> obtenerMaterialesLineaBuscador(string ageOficina, string linCodigo, string like, string cliCodigo, string aplicacion, string matCodigo, int permiso);
    }
}
