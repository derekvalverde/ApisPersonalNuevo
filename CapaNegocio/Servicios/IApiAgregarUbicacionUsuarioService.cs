using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiAgregarUbicacionUsuarioService
    {
        clsAgregarUbicacionUsuarioResponse actualizarAgregarUbicacion(string EmpCodigo, string EmuZona, string EmuDireccion, string EmuLat, string EmuLong, string EmpCodigoIni);
    }
}
