using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiLeccionEliminarService
    {
        clsLeccionEliminarResponse obtenerLeccionEliminar(int lecId);
    }
}
