using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiCambioRegistroService
    {
        clsCambioRegistroResponse actualizarDatosEmpleado(int usuId, string camTabla, string camTipo, string camCampo, int camRegistroId, int camAgrupador, string camAntiguo, string camNuevo);
    }
}
