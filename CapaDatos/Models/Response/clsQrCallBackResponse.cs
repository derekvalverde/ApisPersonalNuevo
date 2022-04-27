using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsQrCallBackResponse
    {
        public string State { get; set; }
        public string Message { get; set; }
        public clsQrDataCallBackResponse Data { get; set; }

    }
}
