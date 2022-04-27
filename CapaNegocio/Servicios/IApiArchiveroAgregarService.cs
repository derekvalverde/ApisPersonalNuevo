using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiArchiveroAgregarService
    {
       clsArchiveroAgregarResponse obteneArchiveroAgregar(string UsuCodigo, string Codigo, string Nombre, string Descripcion, int AreId);
    }
}
