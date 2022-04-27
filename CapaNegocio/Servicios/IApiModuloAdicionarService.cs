using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public interface IApiModuloAdicionarService
    {
        clsModuloAdicionarResponse obtenerModuloAdicionar(int curId, string modTitulo, string modDescripcion, string modEstado, string modImagenDireccion, int usuId);
    }
}
