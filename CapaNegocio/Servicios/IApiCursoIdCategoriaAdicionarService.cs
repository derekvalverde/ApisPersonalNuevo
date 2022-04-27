using CapaDatos.Models.Request;
using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public  interface IApiCursoIdCategoriaAdicionarService
    {
        clsCursoAdicionarResponse registrarCursoIdCategoria(clsCursoAdicionarRequest curso);
    }
}
