using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiInBCP.Models
{
    public class clsQrDataConsultResponse
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public string QrImage { get; set; }
        public string ExpirationDate { get; set; }
        public string Gloss { get; set; }
        public string ServiceCode { get; set; }
        public string BussinessCode { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public List<clsCollector> Collectors { get; set; }
       

        public string OperationNumber { get; set; }
        public string ReceiverAccount { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverDocument { get; set; }


    }
}
