﻿using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiMaterialVentaStokService
    {
        List<clsMaterialVentaStockResponse> obtenerMateriales(int uclId, string like, string cliCodigo, string ageOficina, int usuId, string aplicacion, int permiso);
    }
}
