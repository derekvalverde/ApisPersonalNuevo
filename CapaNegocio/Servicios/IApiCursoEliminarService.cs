using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiCursoEliminarService
    {
        clsCursoEliminarResponse obtenerCursoEliminar(int curId);
    }
}
