using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Request
{
    public class clsCursoAdicionarRequest
    {
       
		public int ticId { get; set; }
		public string curTitulo { get; set; }
		public string curImagenDireccion { get; set; }
		public string curDescripcion { get; set; }
		public int  curDuracionHoras { get; set; }
		public string curEstado { get; set; }
		public int  usuId { get; set; }
		public List<clsCursoCategoriaAdicionarRequest> categoriaAdicionar { get; set; }
	}
}
