using System;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CapaDatos.Models.Response;

namespace CapaNegocio.Servicios
{
    public class ApiActivoLeerService : IApiActivoLeerService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiActivoLeerService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsActivoLeerResponse activoLeer(string actCodigo, int usuId, int empId)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoLeer";

            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.VarChar, 50).Value = actCodigo;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@empId", System.Data.SqlDbType.Int).Value = empId;

            var cantidad = new clsActivoLeerResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                cantidad = MapToValue(reader);
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (cantidad == null)
            {
                return null;
            }
            //Si existe Material
            return cantidad;

        }
        private clsActivoLeerResponse MapToValue(SqlDataReader reader)
        {

            return new clsActivoLeerResponse()
            {
              actCodigo  = reader["actCodigo"].ToString().Trim(),

            };

        }
    }
    
}
