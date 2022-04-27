using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsUsuarioLoginGeneralCardsResponse
    {

		public string usuId { get; set; }	
		public string usuNombre { get; set; }
		public string usuCI { get; set; }
		public string usuTelefono { get; set; }
		public string usuCorreo { get; set; }
		public DateTime usuFecha { get; set; }		
		public string token { get; set; }
	}
}
