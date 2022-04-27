using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsCursoUsuarioModificarRequest
    {
        public int cuuId { get; set; }
        public int usuId { get; set; }
        public int curId { get; set; }
        public decimal cuuScore { get; set; }
        public decimal cuuNota { get; set; }
        public string cuuResena { get; set; }
    }
}
