using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiOutBCP.Models
{
    public class clsQrCallBackRequest
    {
        public string IdCorrelation { get; set; }
        public string CorrelationId { get; set; }
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public string BusinessCode { get; set; }
        public string IdQr { get; set; }
        public string Eif { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Gloss { get; set; }
        public string ReceiverAccount { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverDocument { get; set; }
        public string ReceiverBank { get; set; }
        public string ExpirationDate { get; set; }
        public string ResponseCode { get; set; }

        public string Status { get; set; }
        public string Request { get; set; }
        public DateTime RequestDate { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; }
        public string ResponseAch { get; set; }
        public DateTime ResponseAchDate { get; set; }
        public bool State { get; set; }
       
        public string Description { get; set; }
        public int GenerateType { get; set; }
        public string Version { get; set; }
        public string OperationNumber { get; set; }
        public bool SingleUse { get; set; }
        public string EnableBank { get; set; }
        public string City { get; set; }
        public string BranchOffice { get; set; }
        public string Teller { get; set; }
        public string PhoneNumber { get; set; }
        public List<clsCollector> Collectors { get; set; }

        

    }
}
