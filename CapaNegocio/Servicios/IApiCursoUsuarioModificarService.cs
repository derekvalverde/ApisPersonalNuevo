using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiCursoUsuarioModificarService
    {
        clsCursoUsuarioModificarResponse obtenerCursoUsuarioModificar(int cuuId, int usuId, int curId, decimal cuuScore, decimal cuuNota, string cuuReseña);
    }
}
