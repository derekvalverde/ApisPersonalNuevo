using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiLinkSubgrupoService
    {
        List<clsLinkSubgrupoCabeceraResponse> obtenerLinkSubgrupo(int sgrId);
    }
}
