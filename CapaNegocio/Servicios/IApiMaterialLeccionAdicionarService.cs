using CapaDatos.Models.Request;
using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiMaterialLeccionAdicionarService
    {
        clsMaterialLeccionAdicionarResponse registrarMaterialLeccion(int lecId, string malNombre, string malDireccion, int mtiId);
    }
}
