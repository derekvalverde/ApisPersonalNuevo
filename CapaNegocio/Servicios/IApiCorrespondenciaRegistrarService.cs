using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCorrespondenciaRegistrarService
    {
        clsCorrespondenciaRegistrarResponse registrarCorrespondencia(string UsuCodigo, string CorCodigo, string CorNumGuia, int CodId, int AreId, string CorRemitente, string CorUrgente);
    }
}
