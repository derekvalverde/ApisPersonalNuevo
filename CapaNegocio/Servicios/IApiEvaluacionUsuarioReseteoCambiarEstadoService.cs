using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionUsuarioReseteoCambiarEstadoService
    {
        clsEvaluacionUsuarioReseteoCambiarEstadoResponse obtenerEvaluacionUsuarioReseteoCambiarEstado(int eurId, int usuId, int evlId, string eurEstado);
    }
}
