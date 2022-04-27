using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiActivoBuscarService
    {
        List<clsActivoBuscarResponse> obtenerActivoBuscar(string actCodigo, string actDenomi);
    }
}
