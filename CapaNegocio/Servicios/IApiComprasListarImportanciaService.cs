using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiComprasListarImportanciaService
    {
        List<clsMaterialVentaStockResponse> obtenerPreferencias(string ageOficina, int uclIdld, int comImportancia);
    }
}
