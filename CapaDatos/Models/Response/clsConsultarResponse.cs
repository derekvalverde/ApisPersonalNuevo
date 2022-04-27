using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiInBCP.Models;

namespace CapaDatos.Response
{
    public class clsConsultarResponse
    {
        public string State { get; set; }
        public string Message { get; set; }
        public clsQrDataConsultResponse Data { get; set; }
    }
}
