using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiCorrespondenciaBusquedaService
    {
        List<clsCorrespondenciaBusquedaResponse> obtenerCorrespondenciaBusqueda(string UsuCodigo, string corCodigo, string corRemitente, string areCodigo, DateTime fechaIni, DateTime fechaFin);
    }
}
