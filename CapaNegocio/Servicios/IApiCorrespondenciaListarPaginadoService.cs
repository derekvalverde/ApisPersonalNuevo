using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiCorrespondenciaListarPaginadoService
    {
        List<clsCorrespondenciaListarPaginadoResponse> obteneCorrespondenciaListarPaginado(string UsuCodigo, int corNumPag, int corCantPag);
    }
}
