
using CapaDatos.Response;
using System;
using System.Collections.Generic;


namespace CapaNegocio.Servicios
{
    public interface IApiUsuarioInternoRegistrarImeiService
    {
        List<clsUsuarioInternoRegistrarImeiResponse> obtenerUsuarioInternoRegistrarImei(int usuId, string usuImei);
    }
}
