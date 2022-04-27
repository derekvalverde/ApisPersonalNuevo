using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsEvaluacionUsuarioListarIdResponse
    {
        public int evuId { get; set; }
        public string evuEstadoIntento { get; set; }
        public decimal evuNota { get; set; }
        public int evuNroIntentos { get; set; }
        public DateTime evuFecha { get; set; }
    }
}
