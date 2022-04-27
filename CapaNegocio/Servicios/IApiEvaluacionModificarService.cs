using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiEvaluacionModificarService
    {
        clsEvaluacionModificarResponse obtenerEvaluacionModificar(int evaId, int modId, int tieId, string evaTitulo);
    }
}
