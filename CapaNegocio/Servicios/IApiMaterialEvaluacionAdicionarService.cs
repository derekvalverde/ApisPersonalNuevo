using CapaDatos.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
   public interface IApiMaterialEvaluacionAdicionarService
    {
        clsMaterialEvaluacionAdicionarResponse obtenerMaterialEvaluacionAdicionar(int evaId, string maeNombre, string maeDireccion, string maeEstado, DateTime maeFecha, int mtiId);
    }
}
