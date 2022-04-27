using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionLanzamientoAdicionarService
    {
        clsEvaluacionLanzamientoAdicionarResponse obtenerEvaluacionLanzamientoAdicionar(int evaId, string evlEstado, DateTime evlFecha, int evaLimiteIntentos, DateTime evlFechaInicio, DateTime evlFechaFin, int usuId, int evlDuracion);
    }
}
