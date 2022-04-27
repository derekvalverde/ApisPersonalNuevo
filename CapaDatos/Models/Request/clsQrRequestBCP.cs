using CapaDatos.Models;
using System;
using System.Collections.Generic;


namespace CapaDatos.Request
{
    public class clsQrRequestBCP
    {
        public decimal Amount { get; set; }
        public List<clsCollector> listPedidosAPagar { get; set; }
    }
}
