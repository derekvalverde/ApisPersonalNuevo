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
    public class ApiCorrelativoObtenerService:IApiCorrelativoObtenerService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiCorrelativoObtenerService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrelativoObtenerResponse> obtenerCorrelativo(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CorrelativoObtener";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var correlativo = new List<clsCorrelativoObtenerResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                correlativo.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (correlativo == null)
            {
                return null;
            }

            return correlativo;

        }
        private clsCorrelativoObtenerResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrelativoObtenerResponse()
            {

                corId = Convert.ToInt32(reader["corId"]),

                corPedido = Convert.ToInt32(reader["corPedido"]),
                corRecibo = Convert.ToInt32(reader["corRecibo"]),
                corReciboManual = Convert.ToInt32(reader["corReciboManual"]),
                corReciboManualFinal = Convert.ToInt32(reader["corReciboManualFinal"]),

            };

        }
    }
}
