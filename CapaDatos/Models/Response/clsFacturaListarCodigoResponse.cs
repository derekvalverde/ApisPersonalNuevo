using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsFacturaListarCodigoResponse
    {
	
		public string cliCodigo { get; set; }
		public string facCodigo { get; set; }
		public string facNumero { get; set; }
		public decimal facMonto { get; set; }
		public DateTime facFecha { get; set; }
		public string facFechaPago { get; set; }
		public string  facEmpresa { get; set; }
		public string  matCodigo { get; set; }
		public int fadCantidad { get; set; }
		public decimal fadPrecio { get; set; }
		public decimal fadCheque { get; set; }
		public string  facCategoria { get; set; }
	}
}
