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
    public class ApiMaterialEvaluacionTipoListarService:IApiMaterialEvaluacionTipoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialEvaluacionTipoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialEvaluacionTipoListarResponse> obtenerMaterialEvaluacionTipoListar(int mtiId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialEvaluacionTipoListar";
            cmd.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = mtiId;
            var curso = new List<clsMaterialEvaluacionTipoListarResponse>();
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
        private clsMaterialEvaluacionTipoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialEvaluacionTipoListarResponse()
            {
                evaId = Convert.ToInt32(reader["evaId"]),
                maeNombre = reader["maeNombre"].ToString().Trim(),
                maeDireccion = reader["maeDireccion"].ToString().Trim(),
                maeEstado = reader["maeEstado"].ToString().Trim(),
                maeFecha = Convert.ToDateTime(reader["maeFecha"]),

            };

        }
    }
}
