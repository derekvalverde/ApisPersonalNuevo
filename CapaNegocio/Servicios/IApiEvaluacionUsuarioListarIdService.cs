using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionUsuarioListarIdService
    {
        List<clsEvaluacionUsuarioListarIdResponse> obtenerEvaluacionUsuarioListarId(int usuId, int evlId);
    }
}
