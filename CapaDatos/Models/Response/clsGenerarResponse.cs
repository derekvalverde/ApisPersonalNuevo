using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInBCP.Models;

namespace CapaDatos.Response
{
    public class clsGenerarResponse
    {
        public string State { get; set; }
        public string Message { get; set; }
        public clsQrDataGeneratedResponse Data { get; set; }
    }
}
