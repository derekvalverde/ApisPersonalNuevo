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
    public class ApiQrAdicionarService:IApiQrAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext2 _context;
        public ApiQrAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContext2 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsQRAdicionarResponse obtenerQr(int qrCodigo, string qrImagen, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_QrAdicionar";
            cmd.Parameters.Add("@qrCodigo", System.Data.SqlDbType.Int).Value = qrCodigo;
            cmd.Parameters.Add("@qrImagen", System.Data.SqlDbType.VarChar,40).Value = qrImagen;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var adicionar = new clsQRAdicionarResponse();
            var reader = cmd.ExecuteReader();

            adicionar.adicionado = true;
            
            conn.Close();

            return adicionar;

        }
    }
}
