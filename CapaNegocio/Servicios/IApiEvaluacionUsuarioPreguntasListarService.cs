using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionUsuarioPreguntasListarService
    {
        List<clsEvaluacionUsuarioPreguntasListarResponse> obtenerEvaluacionUsuarioPreguntasListar(int cuuId, int evlId, string evuEstadoIntento);
    }
}
