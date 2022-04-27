using CapaDatos.Data;
using CapaDatos.Models;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;


namespace CapaNegocio.Servicios
{
    public class ApiQrConsultService:IApiQrConsultService
    {
        private readonly AppSettings _appSettings;

        public ApiQrConsultService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }
        public clsQrConsultResponse consultarQR(string idQr)
        {
            try
            {
                Type type = typeof(String);
                clsQrConsult qrConsult = new clsQrConsult
                {
                    Id = idQr,
                    ServiceCode = "050", //Valor Asignado al Servicio de la Empresa
                    BusinessCode = "0138",//Valor Asignado al Empresa
                    AppUserId = " INTIUser08112021",//"INTIUser14072021",//Usuario de Aplicacion asignado a la Empresa
                    PublicToken = " 0CE2B85A-BEBA-4D6A-8009-89CB6656AA7D"//"4DED346F-DB22-4F3A-B035-A4089E1E1930"//Token Publico de la Empresa
                };
                var headers = new SortedDictionary<string, string>
                {
                    { "Correlation-Id", (new Random(10)).Next().ToString() } //Correlation Id debe ser un valor unico enviado por empresa
                };
                clsQrConsultResponse respuestaBCP = clsQrManager.ConsultarQR(
                    string.Format("{0}/api/{1}/Qr/Consult", "https://apis.bcp.com.bo/OpenAPI_Qr/WebApi_Qr/".ToString(), //Url 
                    "V2"),
                    headers, //Cabezera de la Peticion
                    "POST", //Metodo
                    @"C:\INTI.pfx", //Ruta del Certificado
                    "JU$Kjrr$D;RywUGHr", //Contraseña del Certificado
                    qrConsult, // Cuerpo de la Peticion
                    "INTI_USER", //Usuario
                    "JU$Kjrr$D;RywUGHr" //Contraseña usuario
                    );


              
                return respuestaBCP;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
