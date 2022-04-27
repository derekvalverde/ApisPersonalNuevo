using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Request
{
    public class clsQrGeneradoAdicionarRequest
    {
		public string genQrId { get; set; }
		public string solCorrelationId { get; set; }
		public string solServiceCode { get; set; }
		public string solBussinesCode { get; set; }
		public string genIdQrAch { get; set; }
		public string genEif { get; set; }
		public string genAccount { get; set; }
		public decimal solAmount { get; set; }
		public string solCurrency { get; set; }
		public string solGloss { get; set; }
		public string genReceiverAccount { get; set; }
		public string genReceiverName { get; set; }
		public string genReceiverDocument { get; set; }
		public string genReceiverBank { get; set; }
		public string genExpirationDate { get; set; }
		public string genResponseCode { get; set; }
		public string genStatus { get; set; }
		public string genRequest { get; set; }
		public DateTime genRequestDate { get; set; }
		public string genResponse { get; set; }
		public DateTime genResponseDate { get; set; }
		public string genResponseArch { get; set; }
		public DateTime genResponseAchDate { get; set; }
		public string genStateCBRequest { get; set; }
		public string genDescription { get; set; }
		public int genGenerateType { get; set; }
		public string genVersion { get; set; }
		public string genSingleUse { get; set; }
		public string genOperationNumber { get; set; }
		public string genStateCBResponse { get; set; }
		public string genMessageCBResponse { get; set; }
		public string genQrImage { get; set; }
		public string genStateSOResponse { get; set; }
		public string genMessageSOResponse { get; set; }
	}
}
