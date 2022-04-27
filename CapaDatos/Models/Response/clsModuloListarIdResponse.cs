using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsModuloListarIdResponse
    {
        public int  modId { get; set; }
        public string modTitulo { get; set; }
        public string modDescripcion { get; set; }
        public string modImagenDireccion { get; set; }
        public List<clsLeccionListarIdResponse> leccionListarId { get; set; }
        public List<clsEvaluacionListarIdResponse> evaluacionListarId { get; set; }
    }
}
