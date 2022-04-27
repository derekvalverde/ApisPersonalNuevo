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
    public class ApiQrPagoAdicionarService:IApiQrPagoAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext2 _context;
        public ApiQrPagoAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContext2 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsQrPagoAdicionarResponse obtenerPagoQr(int qrCodigo, int clcId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_QrPagoAdicionar";
            cmd.Parameters.Add("@qrCodigo", System.Data.SqlDbType.Int).Value = qrCodigo;
            cmd.Parameters.Add("@clcId ", System.Data.SqlDbType.BigInt).Value = clcId;


            var pagoQr = new clsQrPagoAdicionarResponse();
            var reader = cmd.ExecuteReader();
            pagoQr.adicionado = true;
            conn.Close();

            return pagoQr;

        }
    }
}
