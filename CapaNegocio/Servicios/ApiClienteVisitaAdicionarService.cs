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

namespace CapaNegocio.Servicios
{
    public class ApiClienteVisitaAdicionarService:IApiClienteVisitaAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteVisitaAdicionarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsClienteVisitaAdicionarResponse obtenerVisitaAdicionar(int usuId, int penId, string cliCodigo, float clgLatitud, float clgLongitud, DateTime clgFechaGPS)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
           
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteVisitaAdicionar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@penId", System.Data.SqlDbType.Int).Value = penId;
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar, 10).Value = cliCodigo;
            cmd.Parameters.Add("@clgLatitud", System.Data.SqlDbType.Float).Value = clgLatitud;
            cmd.Parameters.Add("@clgLongitud", System.Data.SqlDbType.Float).Value = clgLongitud;
            cmd.Parameters.Add("@clgFechaGPS", System.Data.SqlDbType.DateTime).Value = clgFechaGPS;


            var adicionar = new clsClienteVisitaAdicionarResponse();
            var reader = cmd.ExecuteReader();
            adicionar.visitaAdicionada = true;

            conn.Close();

            return adicionar;

        }
    }
}
