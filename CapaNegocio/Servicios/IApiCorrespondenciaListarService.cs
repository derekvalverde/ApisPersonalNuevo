using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCorrespondenciaListarService
    {
        List<clsCorrespondenciaListarResponse> obteneCorrespondenciaListar(string UsuCodigo);
    }
}
