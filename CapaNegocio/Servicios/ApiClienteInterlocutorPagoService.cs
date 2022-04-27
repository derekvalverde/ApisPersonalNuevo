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
    public class ApiClienteInterlocutorPagoService:IApiClienteInterlocutorPagoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteInterlocutorPagoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteInterlocutorPagoResponse> obtenerClienteInterlocutor(string cliCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteInterlocutorPago";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar, 10).Value = cliCodigo;
           

            var buscador = new List<clsClienteInterlocutorPagoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                buscador.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (buscador == null)
            {
                return null;
            }
            //Si existe Material


            return buscador;

        }
        private clsClienteInterlocutorPagoResponse MapToValue(SqlDataReader reader)
        {

            return new clsClienteInterlocutorPagoResponse()
            {

                cloInterlocutor = reader["cloInterlocutor"].ToString().Trim(),
               

            };

        }
    }
}
