using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsCursoUsuarioListarReseñaResponse
    {
        public int cuuId { get; set; }
        public string usuNombre { get; set; }
        public string cuuResena { get; set; }
        public decimal cuuScore { get; set; }
        public DateTime cuuFecha { get; set; }
        
    }
}
