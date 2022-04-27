using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsMaterialVentaStockPKResponse
    {
       
		public int matId { get; set; }
		public string matCodigo { get; set; }
		public string matNombre { get; set; }
		public string secCodigo { get; set; }
		public string matEstado { get; set; }
		public int masCantidad { get; set; }
		public decimal marValor { get; set; }
		public string mavLinea { get; set; }
		public string mavOrigen { get; set; }
		public string mavfamilia { get; set; }
		public DateTime masFechaVencimiento { get; set; }
		public int mccId { get; set; }
	}
}
