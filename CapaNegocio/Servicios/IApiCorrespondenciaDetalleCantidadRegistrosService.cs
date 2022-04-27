using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiCorrespondenciaDetalleCantidadRegistrosService
    {
        clsCorrespondenciaDetalleCantidadRegistrosResponse obtenerCorrespondenciaDetalleCantidadRegistros(string UsuCodigo);
    }
}
