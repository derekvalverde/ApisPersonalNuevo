using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiModuloModificarService
    {
        clsModuloModificarResponse obtenerModuloModificar(int modId, string modTitulo, string modDescripcion, string modImagenDireccion);

    }
}
