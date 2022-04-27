using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiEvaluacionUsuarioReseteoAdicionarService
    {
        clsEvaluacionUsuarioReseteoAdicionarResponse obtenerEvaluacionUsuarioReseteoAdicionar(int cuuId, int evlId, string eurDetalle);
    }
}
