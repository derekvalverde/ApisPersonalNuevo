using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiCursoUsuarioNoInscritoBusquedaListarService
    {
        List<clsCursoUsuarioNoInscritoBusquedaListarResponse> obtenerCursoUsuarioNoInscritoBusquedaListar(int ageId, int cargoId, string like, int curId);
    }
}
