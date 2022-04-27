using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiRespuestaAdionarService
    {
        clsRespuestaAdicionarResponse obtenerRespuestaAdicionar(int preId, string resRespuesta, bool resEsCorrecta, string resEstado, DateTime resFecha, int mtiId);
    }
}
