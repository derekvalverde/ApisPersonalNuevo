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
    public class ApiMaterialEvaluacionListarSevice: IApiMaterialEvaluacionListarSevice
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialEvaluacionListarSevice(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialEvaluacionListarResponse> obtenerMaterialEvaluacionListar(int evaId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialEvaluacionListar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            var curso = new List<clsMaterialEvaluacionListarResponse>();
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
        private clsMaterialEvaluacionListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialEvaluacionListarResponse()
            {
                maeId = Convert.ToInt32(reader["maeId"]),
                mtiId = Convert.ToInt32(reader["mtiId"]),
                maeNombre = reader["maeNombre"].ToString().Trim(),
                maeDireccion = reader["maeDireccion"].ToString().Trim(),
                maeEstado = reader["maeEstado"].ToString().Trim(),
                maeFecha = Convert.ToDateTime(reader["maeFecha"]),
                
            };

        }
    }
}
