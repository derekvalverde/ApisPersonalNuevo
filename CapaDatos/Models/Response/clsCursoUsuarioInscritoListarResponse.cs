using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsCursoUsuarioInscritoListarResponse
    {
        public int curId {get; set;}
        public string curTitulo { get; set; }
        public string curImagenDireccion { get; set; }
        public string curDescripcion { get; set; }
        public string curEstado { get; set; }
        public DateTime curFecha { get; set; }
        public decimal curScore { get; set; }
        public string ticNombre { get; set; }
        public int cuuId { get; set; }
    }
}
