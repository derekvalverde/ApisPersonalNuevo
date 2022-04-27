using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiLeccionAdicionarService
    {
        clsLeccionAdicionarResponse obtenerLeccionAdicionar(int modId, string lecTitulo, string lecEstado, int usuId);
    }
}
