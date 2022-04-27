using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiActivoUbicacionTransferirService
    {
        clsActivoUbicacionTransferirResponse obtenerUbicacionTransferir(string actCodigo, string usuCodigo);
    }
}
