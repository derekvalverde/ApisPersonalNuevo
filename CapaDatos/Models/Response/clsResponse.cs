using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsResponse
    {
        public string State { get; set; }
        public string Message { get; set; }
        public clsQrResponse Data { get; set; }
    }
}
