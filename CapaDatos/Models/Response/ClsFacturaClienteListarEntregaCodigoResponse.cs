using System;

namespace CapaDatos.Request
{
    public class ClsFacturaClienteListarEntregaCodigoResponse
    {
        public string cliCodigo { get; set; }
        public string facCodigo { get; set; }
        public string facNumero { get; set; }
        public double facMonto { get; set; }
        public DateTime facFecha { get; set; }
        public DateTime facFechaPago { get; set; }
        public string facEmpresa { get; set; }
        public string facCategoria { get; set; }
        public string color { get; set; } 
    }
}