using CapaDatos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class clsQrModels
    {
        public List<clsCollector> Collectors { get; set; }
        public string AppUserId { get; set; }
        public string PublicToken { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string Gloss { get; set; }
        public string ServiceCode { get; set; }
        public string BusinessCode { get; set; }
        public string Expiration { get; set; }
        public bool SingleUse { get; set; }
        public string EnableBank { get; set; }
        public string City { get; set; }
        public string BranchOffice { get; set; }
        public string Teller { get; set; }
        public string PhoneNumber { get; set; }

    }
}
