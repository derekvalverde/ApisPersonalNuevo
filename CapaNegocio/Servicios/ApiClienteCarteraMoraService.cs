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
    public class ApiClienteCarteraMoraService : IApiClienteCarteraMoraService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteCarteraMoraService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsClienteCarteraMoraBoolResponse obtenerMora(string cliCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteCarteraMora";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.Int).Value = cliCodigo;


            var mora= new clsClienteCarteraMoraBoolResponse();
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                mora.tieneMora = true;
            }
            else
            {
                mora.tieneMora = false;
            }
            conn.Close();                              


            return mora;

        }
       
    }
}
