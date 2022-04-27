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
    public class ApiMaterialLeccionListarService: IApiMaterialLeccionListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialLeccionListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialLeccionListarResponse> obtenerMaterialLeccionListar(int lecId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialLeccionListar";
            cmd.Parameters.Add("@lecId", System.Data.SqlDbType.Int).Value = lecId;
            var curso = new List<clsMaterialLeccionListarResponse>();
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
        private clsMaterialLeccionListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialLeccionListarResponse()
            {
                malId = Convert.ToInt32(reader["malId"]),
                malNombre = reader["malNombre"].ToString().Trim(),
                malDireccion = reader["malDireccion"].ToString().Trim(),
                malFecha = Convert.ToDateTime(reader["malFecha"]),
                mtiId = Convert.ToInt32(reader["mtiId"]),
            };

        }
    }
}
