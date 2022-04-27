using CapaDatos.Data;
using CapaDatos.Models.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiEvaluacionIdUsuarioListarService: IApiEvaluacionIdUsuarioListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionIdUsuarioListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEvaluacionIdUsuarioListarResponse> obtenerEvaluacionIdUsuarioListar(int evaId, string like)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionIdUsuarioListar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            cmd.Parameters.Add("@like", System.Data.SqlDbType.NVarChar, 50).Value = like;
            var curso = new List<clsEvaluacionIdUsuarioListarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                curso.Add(MapToValue(reader));
            }
            conn.Close();

            if (curso == null)
            {
                return null;
            }

            return curso;

        }
        private clsEvaluacionIdUsuarioListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEvaluacionIdUsuarioListarResponse()
            {
                usuId = Convert.ToInt32(reader["usuId"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),                
                evlFecha = Convert.ToDateTime(reader["evlFecha"]),
                
            };

        }
    }
}
