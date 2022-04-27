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
    public class ApiChequeListarPendienteService: IApiChequeListarPendienteService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiChequeListarPendienteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsChequeListarPendienteResponse> obtenerChequePendiente(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ChequeListarPendiente";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var chequePendiente = new List<clsChequeListarPendienteResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
               chequePendiente.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (chequePendiente == null)
            {
                return null;
            }

            return chequePendiente;

        }
        private clsChequeListarPendienteResponse MapToValue(SqlDataReader reader)
        {

            return new clsChequeListarPendienteResponse()
            {

                cheId = Convert.ToInt32(reader["cheId"]),
                choDetalle = reader["choDetalle"].ToString().Trim(),
                chdId = Convert.ToInt32(reader["chdId"]),
                cheMonto = Convert.ToDecimal(reader["cheMonto"]),
                cheFecha = Convert.ToDateTime(reader["cheFecha"]),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),

            };

        }
    }
}
