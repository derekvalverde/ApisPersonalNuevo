using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiArchiveroCantidadRegistrosService
    {
        clsArchiveroCantidadRegistrosResponse obtenerArchiveroCantidadRegistros(string UsuCodigo);
    }
}
