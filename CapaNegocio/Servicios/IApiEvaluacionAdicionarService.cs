using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionAdicionarService
    {
        clsEvaluacionAdicionarResponse obtenerEvaluacionAdicionar(int modId, int tieId, string evaTitulo, string evaEstado, DateTime evaFecha, int usuId);
    }
}
