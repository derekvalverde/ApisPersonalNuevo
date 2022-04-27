using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiAgregarEstudioUsuarioService
    {
        clsAgregarEstudioUsuarioResponse actualizarEstudiosUsuario(string EmpCodigo, string EstInstitucion, DateTime EstFecha, DateTime EstFechaFin, string EstExplicacion, string EstNombre, string EstNivel, int EttId, string EmpCodigoIni);
    }
}
