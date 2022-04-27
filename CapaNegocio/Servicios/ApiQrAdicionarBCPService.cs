using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapaDatos.Data;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebIntiApi.Models;
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public class ApiQrAdicionarBCPService: IApiQrAdicionarBCPService
    {

        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext2 _context;
        public ApiQrAdicionarBCPService(IOptions<AppSettings> appSettings, AplicacionDbContext2 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsQrAdicionarBCPResponse adicionarSolicitudQr(clsQrAdicionarBCPRequest qr)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            //Adiciono la solicitud
            cmd.CommandText = "Api_QrAdicionar";
            cmd.Parameters.Add("@qrIdGenerado", System.Data.SqlDbType.VarChar,20).Value = qr.qrIdGenerado;
            cmd.Parameters.Add("@correlation", System.Data.SqlDbType.VarChar,50).Value = qr.correlation;
            cmd.Parameters.Add("@moneda", System.Data.SqlDbType.VarChar,45).Value = qr.moneda;
            cmd.Parameters.Add("@importe", System.Data.SqlDbType.Decimal).Value = qr.importe;
            cmd.Parameters.Add("@glosa", System.Data.SqlDbType.VarChar,60).Value = qr.glosa;
            cmd.Parameters.Add("@serviceCode", System.Data.SqlDbType.VarChar,6).Value = qr.serviceCode;
            cmd.Parameters.Add("@bussinesCode", System.Data.SqlDbType.VarChar, 4).Value = qr.bussinesCode;
            
            var reader = cmd.ExecuteReader();
     
            var respuesta = "";    // 0 Ejecuta procedimento almacenado;   <>0 no Ejecuta procedmiento almacendado
            
            while (reader.Read())
            {
                respuesta = reader["respuesta"].ToString().Trim();
            }
            //Si adiciono la solicitud del detalle
            if (respuesta == "si")
            { 
                foreach (clsQrAdicionarDetalleBCPRequest factura in qr.detalleSolicitudAdicionar)
                {
                    SqlCommand cmd1 = conn.CreateCommand();
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.CommandText = "Api_QrDetalleAdicionar";
                    cmd1.Parameters.Add("@facMonto", System.Data.SqlDbType.Decimal).Value = factura.facMonto;
                    cmd1.Parameters.Add("@sodNombre", System.Data.SqlDbType.VarChar, 45).Value = factura.sodNombre;
                    cmd1.Parameters.Add("@facNumero", System.Data.SqlDbType.VarChar,45).Value = factura.facNumero;
                    cmd1.Parameters.Add("@correlation", System.Data.SqlDbType.VarChar, 50).Value = factura.correlation;
                    reader = cmd1.ExecuteReader();                    
                }                
            }
            clsQrAdicionarBCPResponse resp = new clsQrAdicionarBCPResponse();
            resp.qrSolicitudAdicionada = true;
            return resp;
        }



    }
}
