using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsMaterialVentaStockPKDiarioResponse
    {
		
		public string matCodigo { get; set; }
		
		public int masCantidad { get; set; }
		public decimal marValor { get; set; }
		
		
		public DateTime masFechaVencimiento { get; set; }
	}
}
