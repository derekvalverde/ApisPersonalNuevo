using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
   public class clsApiCursoUsuarioListarResponse
    {
        public int curId { get; set; }
        public string curTitulo { get; set; }
        public int curImagenDireccion { get; set; }
        public int curDescripcion { get; set; }
        public int curEstado { get; set; }
        public DateTime curFecha { get; set; }
        public int curScore { get; set; }
        public string ticNombre { get; set; }
    }
}
