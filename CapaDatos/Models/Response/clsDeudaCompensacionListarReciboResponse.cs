using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsDeudaCompensacionListarReciboResponse
    {
        public int recId { get; set; }
        public int decId { get; set; }
        public decimal decMonto { get; set; }
        public DateTime decFecha { get; set; }
        public string cliNombreComercial { get; set; }
        public string cliNombreFiscal { get; set; }
        public string cliCodigo { get; set; }
        public int dedNumero { get; set; }
        public string cliIdentificacionNumero { get; set; }
        public string cliCiudad { get; set; }

        public string cliDireccionComercial { get; set; }
        public List<clsDeudaCompensacionListarReciboDetalleResponse> detalleRecibo { get; set; }

    }
}
