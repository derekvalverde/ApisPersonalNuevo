using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiLeccionModificarService
    {
        clsLeccionModificarResponse obtenerLeccionModificar(int lecId, int modId, string lecTitulo);
    }
}
