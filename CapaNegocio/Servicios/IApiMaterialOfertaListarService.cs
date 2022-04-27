using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiMaterialOfertaListarService
    {
        List<clsMaterialVentaStockResponse> obtenerOfertas(string ageOficina,string cliCodigo);
        
    }
}
