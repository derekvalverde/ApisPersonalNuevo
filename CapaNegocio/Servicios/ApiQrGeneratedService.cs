using CapaDatos.Data;
using CapaDatos.Models;
using CapaDatos.Request;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace CapaNegocio.Servicios
{
    public class ApiQrGeneratedService:IApiQRGeneratedService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextQR _context;
        private readonly ILogger<clsQrManager> _logger;

        public ApiQrGeneratedService(IOptions<AppSettings> appSettings, ApplicationDbContextQR context, ILogger<clsQrManager> logger)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _logger = logger;
        }
        public clsQrGeneratedResponse generarQR(Decimal monto, string CorrelationId, List<clsCollector> listaPedidosAPagar, List<clsQrAdicionarDetalleBCPRequest> detalleSolicitudAdicionar)
        {
            try
            {
                Type type = typeof(String);
                clsQrModels qrModels = new clsQrModels
                {
                    Amount = monto,//Monto de la Transaccion
                    Currency = "BOB",//Moneda de la Transaccion
                    Collectors = listaPedidosAPagar,
                    Expiration = "00/00:00",//Valor fijo que se debe enviar 00/00:00 default
                    Gloss = "Pago",//Descripcion de la Transaccion
                    ServiceCode = "050", //Valor Asignado al Servicio de la Empresa
                    BusinessCode = "0169",//Valor Asignado al Empresa
                    AppUserId = "INTIUser14072021",//Usuario de Aplicacion asignado a la Empresa
                    PublicToken = "4DED346F-DB22-4F3A-B035-A4089E1E1930",//Token Publico de la Empresa
                    SingleUse = true,
                    EnableBank = "ALL",
                    City = "LA PAZ",
                    BranchOffice = "SUCURSAL CENTRO",
                    Teller = "CAJA 1",
                    PhoneNumber = "132456798",


                };
                var headers = new SortedDictionary<string, string>
                {
                    { "Correlation-Id", CorrelationId } //Correlation Id debe ser un valor unico enviado por empresa
                };
                clsQrManager objQrManager = new clsQrManager(_logger);
                clsGenerarResponse respuestaBCP = objQrManager.Conexion(
                    string.Format("{0}/api/{1}/Qr/Generated", "https://www99.bancred.com.bo/sandbox".ToString(), //Url
                    "v4"),
                    headers, //Cabezera de la Peticion
                    "POST", //Metodo       
                           @"E:\ALICIA\CertificateSandBox.pfx", //Ruta del Certificado
                    "Pa$$Bcp2021", //Contraseña del Certificado

                    qrModels, // Cuerpo de la Peticion
                   "INTI_USER", //Usuario
                    "ed6ce73a0234d2b45876abd2494bb53a" //Contraseña usuario
                    );
                               
                
                clsQrGeneratedResponse qrResponse = new clsQrGeneratedResponse();
                qrResponse.respuesta = respuestaBCP;

                //
                // Guardar QR Generado en base de datos ERPQR
                //
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "Api_QrAdicionar";
                cmd.Parameters.Add("@qrIdGenerado", System.Data.SqlDbType.VarChar, 20).Value = qrResponse.respuesta.Data.Id;
                cmd.Parameters.Add("@correlation", System.Data.SqlDbType.VarChar, 50).Value = CorrelationId;
                cmd.Parameters.Add("@moneda", System.Data.SqlDbType.VarChar, 45).Value = "BOB";
                cmd.Parameters.Add("@importe", System.Data.SqlDbType.Decimal).Value = monto;
                cmd.Parameters.Add("@glosa", System.Data.SqlDbType.VarChar, 60).Value = "Pago";
                cmd.Parameters.Add("@serviceCode", System.Data.SqlDbType.VarChar, 6).Value = "050";
                cmd.Parameters.Add("@bussinesCode", System.Data.SqlDbType.VarChar, 4).Value = "0138";



                var reader = cmd.ExecuteReader();

                var respuesta = "";    // 0 Ejecuta procedimento almacenado;   <>0 no Ejecuta procedmiento almacendado

                while (reader.Read())
                {
                    respuesta = reader["respuesta"].ToString().Trim();
                }
                //Si adiciono la solicitud del detalle
                if (respuesta == "si")
                {
                    foreach (clsQrAdicionarDetalleBCPRequest factura in detalleSolicitudAdicionar)
                    {
                        SqlCommand cmd1 = conn.CreateCommand();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Api_QrDetalleAdicionar";
                        cmd1.Parameters.Add("@facMonto", System.Data.SqlDbType.Decimal).Value = factura.facMonto;
                        cmd1.Parameters.Add("@sodNombre", System.Data.SqlDbType.VarChar, 45).Value = factura.sodNombre;
                        cmd1.Parameters.Add("@facNumero", System.Data.SqlDbType.VarChar, 45).Value = factura.facNumero;
                        cmd1.Parameters.Add("@correlation", System.Data.SqlDbType.VarChar, 50).Value = factura.correlation;
                        reader = cmd1.ExecuteReader();
                    }
                }

                return qrResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
