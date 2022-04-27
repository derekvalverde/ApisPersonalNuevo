using CapaDatos.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiTipoEvaluacionListarService
    {
        List<clsTipoEvaluacionlistarResponse> obtenerTipoEvaluacionlistar();
    }
}
