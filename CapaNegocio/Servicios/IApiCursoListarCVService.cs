using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiCursoListarCVService
    {
        List<clsCursoListarCVResponse> obtenerCursoListar(int usuId, string curEstado);
    }
}
