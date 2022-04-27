using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiCursoUsuarioInscritoBusquedaListarService
    {
        List<clsCursoUsuarioInscritoBusquedaListarResponse> obtenerCursoUsuarioInscritoBusquedaListar(int ageId, int cargoId, string like, int curId);
    }
}
