using CapaDatos.Models;
using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiDescuentosService
    {
        clsDescuentosResponse obtenerDescuento(string ageOficina, string cliCodigo, string mavLinea, string mavOrigen, string matCodigo, string canal, int usuId);
    }
}
