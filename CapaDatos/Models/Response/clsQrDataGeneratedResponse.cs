using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiInBCP.Models
{
    public class clsQrDataGeneratedResponse
    {
        public string Id { get; set; }
        public string QrImage { get; set; }
        public string ExpirationDate { get; set; }
    }
}
