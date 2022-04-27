using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiCursoModificarService
    {
        clsCursoModificarResponse obtenerCursoModificar(int curId, int ticId, string curTitulo, string curImagenDireccion, string curDescripcion, int curDuracionHoras, string curEstado);
    }
}
