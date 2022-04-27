using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiAgregarFamiliarUsuarioService
    {
        clsAgregarFamiliarUsuarioResponse actualizarFamiliaresUsuario(string EmpCodigo, string FamNombre, int FamCarnet, string FamCarnetExt, int FamCelular, string FamTipo, DateTime FamNacimiento, string FamSexo, string FamGradoActual, int FarmCursoActual, string EmpCodigoIni);

    }
}
