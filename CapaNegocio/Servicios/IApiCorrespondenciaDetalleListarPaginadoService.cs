using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiCorrespondenciaDetalleListarPaginadoService
    {
        List<clsCorrespondenciaDetalleListarPaginadoResponse> obteneCorrespondenciaDetalleListarPaginado(string UsuCodigo, int cdeNumPag, int cdeCantReg);
    }
}
