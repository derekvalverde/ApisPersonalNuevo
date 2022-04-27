using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
	public class clsCompraListarUsuIdResponse
	{
		
		public int comId { get; set; }
		public string cliCodigo { get; set; }
		public string matCodigo { get; set; }
		public decimal comPromedio { get; set; }
		public int comCantidadPromedio { get; set; }
		public int comImportancia { get; set; }
	}
}


