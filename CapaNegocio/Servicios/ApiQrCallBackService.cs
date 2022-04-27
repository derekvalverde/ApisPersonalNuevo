using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApiOutBCP.Models;
using Microsoft.Extensions.Logging;
using CapaDatos.Data;
using CapaDatos.Request;
using CapaDatos.Response;
using CapaNegocio.Hubs;
using System.Net;

namespace CapaNegocio.Servicios
{
    public class ApiQrCallBackService:IApiQrCallBackService
    {
        private readonly AppSettings _appSettings;
        private readonly IHubContext<clsCallbackHub> hub;
        private readonly ILogger<ApiQrCallBackService> _logger;

        public ApiQrCallBackService(IHubContext<clsCallbackHub> hub, IOptions<AppSettings> appSettings, ILogger<ApiQrCallBackService> logger)
        {
            this.hub = hub;
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        public async Task<clsQrCallBackResponse> verificarPago(clsQrCallBackRequest model)
        {
            clsLoginRequest loginRequest = new clsLoginRequest();
            loginRequest.Usuario = "sis";
            loginRequest.Contra = "73227812";            
            loginRequest.Imei = "1";
            loginRequest.GrpId = 1;
            //Obtenemos token INTI
            var UsuarioGenerico = obtenerLoginInti(loginRequest);
            //Con el token consumo Api ApiQrGeneradoAdicionar
            clsQrAdicionarCallbackRequest qrGeneradoRequest = new clsQrAdicionarCallbackRequest();

            /************                                              
        
        
        public string calVersion { get; set; }        public string calDescription { get; set; }
        public int calGenerateType { get; set; }        public string calSingleUse { get; set; }
        public string calOperationNumber { get; set; }        public string calEnableBlack { get; set; }
        public string calCity { get; set; }        public string calTeller { get; set; }
        public string calBrachOffice { get; set; }        public string calPhoneNumber { get; set; }
        /*****************/
            qrGeneradoRequest.qrIdGenerado = model.Id.ToString();
            qrGeneradoRequest.calCorrelationId = model.CorrelationId;
            qrGeneradoRequest.calServiceCode = model.ServiceCode;
            qrGeneradoRequest.calBussinesCode = model.BusinessCode;
            qrGeneradoRequest.calIdQrAch = model.IdQr;
            qrGeneradoRequest.calEif = model.Eif;
            qrGeneradoRequest.calAccount = model.Account;
            qrGeneradoRequest.calAmount = model.Amount.ToString();
            qrGeneradoRequest.calCurrency = model.Currency;
            qrGeneradoRequest.calGloss = model.Gloss;
            qrGeneradoRequest.calReceiverAccount = model.ReceiverAccount;
            qrGeneradoRequest.calReceiverName = model.ReceiverName;
            qrGeneradoRequest.calReceiverDocument = model.ReceiverDocument;
            qrGeneradoRequest.calReceiverBank = model.ReceiverBank;
            qrGeneradoRequest.calExpirationDate = model.ExpirationDate;
            qrGeneradoRequest.calResponseCode = model.ResponseCode;
            qrGeneradoRequest.calStatus = model.Status;
            qrGeneradoRequest.calRequest = model.Request;
            qrGeneradoRequest.calRequestDate = model.RequestDate;
            qrGeneradoRequest.calResponse = model.Response;
            qrGeneradoRequest.calResponseDate = model.ResponseDate;
            qrGeneradoRequest.calResponseArch = model.ResponseAch;
            qrGeneradoRequest.calResponseAchDate = model.ResponseAchDate;
            qrGeneradoRequest.calVersion = model.Version;
            qrGeneradoRequest.calDescription = model.Description;
            qrGeneradoRequest.calGenerateType = model.GenerateType;
            qrGeneradoRequest.calSingleUse = model.SingleUse.ToString();
            qrGeneradoRequest.calOperationNumber = model.OperationNumber;
            qrGeneradoRequest.calEnableBlack = model.EnableBank;
            qrGeneradoRequest.calCity = model.City;
            qrGeneradoRequest.calTeller = model.Teller;
            qrGeneradoRequest.calBrachOffice = model.BranchOffice;
            qrGeneradoRequest.calPhoneNumber = model.PhoneNumber;
            qrGeneradoRequest.IdCorrelation = model.IdCorrelation;


            /****************************/

            var qrGeneradoAdicionado = grabarQrGeneradoAdicionar(qrGeneradoRequest, UsuarioGenerico.Token);

            String estado = "010";
            String mensajeRespuesta = "Error general";

            
            Console.WriteLine("Generado adicionado");
            Console.WriteLine(qrGeneradoAdicionado.qrAdicionado);

            if (qrGeneradoAdicionado.qrAdicionado == 1)
            {
                mensajeRespuesta = "Correcto";
                estado = "000";
            }
            else if (qrGeneradoAdicionado.qrAdicionado == -1)
            {
                mensajeRespuesta = "Correlation Id incorrecto";
                estado = "001";
            }
            else if (qrGeneradoAdicionado.qrAdicionado == -2)
            {
                mensajeRespuesta = "QR Id incorrecto";
                estado = "002";
            }
            else if (qrGeneradoAdicionado.qrAdicionado == -3)
            {
                mensajeRespuesta = "QR ya existente";
                estado = "003";
            }

            Console.WriteLine("Estado: {0}", estado);
            Console.WriteLine("Mensaje: {0}", mensajeRespuesta);

            clsQrCallBackResponse respuesta = new clsQrCallBackResponse();
            //Verificar los campos del model importe > 0 idQR que sea uno de los que inti genero
            respuesta.State = estado;
            respuesta.Message = mensajeRespuesta;
            clsQrDataCallBackResponse dataResponse = new clsQrDataCallBackResponse();
            dataResponse.Id = model.Id.ToString();
            respuesta.Data = dataResponse;

            //postproceso
            //
            
            //
            // Signal R
            //
            await hub.Clients.All.SendAsync("RecibirCallback", respuesta.State, respuesta.Message, dataResponse.Id);
            return respuesta;

        }
        public clsLoginResponse obtenerLoginInti(clsLoginRequest login)
        {            
            clsLoginResponse loginResponse = new clsLoginResponse();
            
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //Api Login 
            string url = ApiIntiURL + "serviciosPocket/ApiUsuario/UsuarioLoginGeneralPocket"; 


            clsLoginResponse datoscliente = new clsLoginResponse();
            var vm = new { usuLogin = login.Usuario, usuPassword = login.Contra, usuImei = login.Imei, grpId = 1 };
            using (var client = new WebClient())
            {
                Console.WriteLine("Prueba");
                Console.WriteLine(url);

                var dataString = JsonConvert.SerializeObject(vm);
                Console.WriteLine(dataString);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var respuesta = client.UploadString(new Uri(url), "POST", dataString);

                datoscliente = JsonConvert.DeserializeObject<clsLoginResponse>(respuesta);
                Console.WriteLine("Prueba 2");
            }
            loginResponse = datoscliente;


            return loginResponse;

        }


        public clsQrAdicionarCallbackResponse grabarQrGeneradoAdicionar(clsQrAdicionarCallbackRequest model, string cadToken)
        {
            clsLoginResponse loginResponse = new clsLoginResponse();
            var ApiIntiURL = _appSettings.ApiIntiURL;
            //Api Login 
            string url = ApiIntiURL + "serviciosPocket/ApiQrAdicionarCallback";
            //fecha modiicada    
            DateTime NuevaCalRequestDate;
            DateTime NuevaCalResponseDate;
            DateTime NuevaCalResponseAchDate;
            if (model.calRequestDate <= Convert.ToDateTime("0002-01-01"))
            {
                NuevaCalRequestDate = Convert.ToDateTime("1901-01-01");

            }
            else
            {
                NuevaCalRequestDate = model.calRequestDate;
            }

            if (model.calResponseDate <= Convert.ToDateTime("0002-01-01"))
            {
                NuevaCalResponseDate = Convert.ToDateTime("1901-01-01");

            }
            else
            {
                NuevaCalResponseDate = model.calResponseDate;
            }

            if (model.calResponseAchDate <= Convert.ToDateTime("0002-01-01"))
            {
                NuevaCalResponseAchDate = Convert.ToDateTime("1901-01-01");

            }
            else
            {
                NuevaCalResponseAchDate = model.calResponseAchDate;
            }


            clsQrAdicionarCallbackResponse callBackGrabado = new clsQrAdicionarCallbackResponse();
            var vm = new
            {
                qrIdGenerado = model.qrIdGenerado,
                calCorrelationId = model.calCorrelationId,
                calServiceCode = model.calServiceCode,
                calBussinesCode = model.calBussinesCode,
                calIdQrAch = model.calIdQrAch,
                calEif = model.calEif,
                calAccount = model.calAccount,
                calAmount = model.calAmount.ToString(),
                calCurrency = model.calCurrency,
                calGloss = model.calGloss,
                calReceiverAccount = model.calReceiverAccount,
                calReceiverName = model.calReceiverName,
                calReceiverDocument = model.calReceiverDocument,
                calReceiverBank = model.calReceiverBank,
                calExpirationDate = model.calExpirationDate,
                calResponseCode = model.calResponseCode,
                calStatus = model.calStatus,
                calRequest = model.calRequest,

                calRequestDate = NuevaCalRequestDate,
                calResponse = model.calResponse,
                calResponseDate = NuevaCalResponseDate,
                calResponseArch = model.calResponseArch,
                calResponseAchDate = NuevaCalResponseAchDate,
                calDescription = model.calDescription,
                calGenerateType = model.calGenerateType,
                calVersion = model.calVersion,
                calOperationNumber = model.calOperationNumber,
                calSingleUse = model.calSingleUse,
                calEnableBlack = model.calEnableBlack,
                calCity = model.calCity,
                calBrachOffice = model.calBrachOffice,
                calTeller = model.calTeller,
                calPhoneNumber = model.calPhoneNumber,
                IdCorrelation = model.IdCorrelation

            };
            using (var client = new WebClient())
            {
                var dataString = JsonConvert.SerializeObject(vm);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cadToken;
                var respuesta = client.UploadString(new Uri(url), "POST", dataString);
                callBackGrabado = JsonConvert.DeserializeObject<clsQrAdicionarCallbackResponse>(respuesta);
            }
            return callBackGrabado;

        }

    }
}
