using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsQrAdicionarCallbackRequest
    {
		public string qrIdGenerado { get; set; }
		public string calCorrelationId { get; set; }
		public string calServiceCode { get; set; }
		public string calBussinesCode { get; set; }
		public string calIdQrAch { get; set; }
		public string? calEif { get; set; }
		public string calAccount { get; set; }
		public string calAmount { get; set; }
		public string calCurrency { get; set; }
		public string calGloss { get; set; }
		public string calReceiverAccount { get; set; }
		public string calReceiverName { get; set; }
		public string calReceiverDocument { get; set; }
		public string calReceiverBank { get; set; }
		public string calExpirationDate { get; set; }
		public string calResponseCode { get; set; }
		public string calStatus { get; set; }
		public string calRequest { get; set; }
		public DateTime calRequestDate { get; set; }
		public string calResponse { get; set; }
		public DateTime calResponseDate { get; set; }
		public string calResponseArch { get; set; }
		public DateTime calResponseAchDate { get; set; }
		public string calVersion { get; set; }
		public string calDescription { get; set; }
		public int calGenerateType { get; set; }

		public string calSingleUse { get; set; }
		public string calOperationNumber { get; set; }
		public string calEnableBlack { get; set; }
		public string calCity { get; set; }
		public string calTeller { get; set; }
		public string calBrachOffice { get; set; }
		public string calPhoneNumber { get; set; }
		public string IdCorrelation { get; set; }


	}
}
