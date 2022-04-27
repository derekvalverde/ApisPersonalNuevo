using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsMaterialStockCodigoAlmacenResponse
    {
		public int matId { get; set; }
		public string matCodigo { get; set; }
		public int masCantidad { get; set; }
		public string almCodigo{ get; set; }
		public string masEstado { get; set; }
		public string masLote { get; set; }
		public DateTime masFechaVencimiento { get; set; }

		
    }
}
