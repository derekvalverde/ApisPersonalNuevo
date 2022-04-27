using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsCursoListarCVResponse
    {
        public int curId { get; set; }
        public string curTitulo { get; set; }
        public string curDescripcion { get; set; }
        public int curDuracionHoras { get; set; }
        public DateTime curFecha { get; set; }
        public string curImagenDireccion { get; set; }
        public decimal curScore { get; set; }
        
        public int ticId { get; set; }
        public int nroInscritos { get; set; }

    }
}
