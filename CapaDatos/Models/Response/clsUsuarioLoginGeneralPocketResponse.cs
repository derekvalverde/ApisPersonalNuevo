using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsUsuarioLoginGeneralPocketResponse
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
		public string usuVersion { get; set; }
		public int usuDia { get; set; }
		public string token { get; set; }
	}
}
