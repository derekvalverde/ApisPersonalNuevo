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
    public class ApiQrModificarQrIdBCPService: IApiQrModificarQrIdBCPService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext2 _context;
        public ApiQrModificarQrIdBCPService(IOptions<AppSettings> appSettings, AplicacionDbContext2 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsQrModificarQrIdBCPResponse modificarQrId(clsQrModificarQrIdBCPRequest qr)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Adiciono la solicitud
            cmd.CommandText = "Api_QrModificarQrId";
            cmd.Parameters.Add("@qrIdGenerado", System.Data.SqlDbType.VarChar, 20).Value = qr.qrIdGenerado;
            cmd.Parameters.Add("@qrCorrelationId", System.Data.SqlDbType.VarChar, 50).Value = qr.qrCorrelationId;
            
            var reader = cmd.ExecuteReader();

            var respuesta = "";    // 0 Ejecuta procedimento almacenado;   <>0 no Ejecuta procedmiento almacendado

            while (reader.Read())
            {
                respuesta = reader["respuesta"].ToString().Trim();
            }
            //Si adiciono la solicitud del detalle
            clsQrModificarQrIdBCPResponse resp = new clsQrModificarQrIdBCPResponse();
            if (respuesta == "si")
            {               
                resp.qrIdModificado = true;            
            }
            else {
                resp.qrIdModificado = false;            
            }
            return resp;
        }

    }
}
