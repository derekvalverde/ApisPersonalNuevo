using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsEvaluacionIdUsuarioListarResponse
    {
        public int usuId { get; set; }
        public string  usuNombre { get; set; }
        public DateTime evlFecha { get; set; }
    }
}
