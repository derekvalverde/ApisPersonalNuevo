using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiArchiveroListarPaginadoService
    {
        List<clsArchiveroListarPaginadoResponse> obtenerArchiveroListarPaginado(string UsuCodigo, int arcNumPag, int arcCantReg);
    }
}
