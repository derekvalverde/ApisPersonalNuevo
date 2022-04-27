using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCorrespondenciaDetalleDividirService
    {

        clsCorrespondenciaDetalleDividirResponse obtenerCorrespondenciaDetalleDividir(string usuCodigo, int corId, string cdeCodigo, int usuFinalId, string cdeDetalle, string cdeUrgente);

    }
}
