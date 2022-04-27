using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Response
{
    public class clsUsuarioLoginGeneralResponse
    {
		public int usuId { get; set; }
		public int ageId { get; set; }
		public string usuCodigo { get; set; }
		public string usuNombre { get; set; }
		public string sgrTipoNombre { get; set; }
		public string grpInicio { get; set; }

		public int sgrId { get; set; }
		public int uscId { get; set; }
		public string ageOficina { get; set; }

		public string token { get; set; }
	}
}
