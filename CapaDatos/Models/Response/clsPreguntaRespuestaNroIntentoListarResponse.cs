using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsPreguntaRespuestaNroIntentoListarResponse
    {
        public int preId { get; set; }
        public string prePregunta { get; set; }
        public int resId { get; set; }
        public string resRespuesta { get; set; }
        public string resEsCorrecta { get; set; }
    }
}
