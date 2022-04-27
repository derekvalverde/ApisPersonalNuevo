using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiCursoUsuarioResponsableAdicionarService
    {
        clsCursoUsuarioResponsableAdicionarResponse obtenerCursoUsuarioResponsableAdicionar(int usuId, int curId, string curEstado);
    }
}
