using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionRespuestaUsuarioAdicionarService
    {
        clsEvaluacionRespuestaUsuarioAdicionarResponse obtenerEvaluacionRespuestaUsuarioAdicionar(int resId, int evuId, string eruEstado);
    }
}
