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
   public class ApiCursoUsuarioResponsableListarService: IApiCursoUsuarioResponsableListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioResponsableListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoUsuarioResponsableListarResponse> obtenerCursoUsuarioResponsableListar(int curId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioResponsableListar";
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
            var curso = new List<clsCursoUsuarioResponsableListarResponse>();
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
        private clsCursoUsuarioResponsableListarResponse MapToValue(SqlDataReader reader)
        {
            return new clsCursoUsuarioResponsableListarResponse()
            {
                curId = Convert.ToInt32(reader["curId"]),
                usuId = Convert.ToInt32(reader["usuId"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),                
            };
        }
    }
}
