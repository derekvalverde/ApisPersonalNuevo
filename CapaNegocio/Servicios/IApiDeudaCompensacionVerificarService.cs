using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using System;
using System.Collections.Generic;


namespace CapaNegocio.Servicios
{
    public interface IApiDeudaCompensacionVerificarService
    {
        List<clsDeudaCompensacionVerificarResponse> obtenerDeudaCompensacionVerificar(int usuId);
    }
}
