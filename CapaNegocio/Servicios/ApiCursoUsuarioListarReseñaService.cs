using CapaDatos.Data;
using CapaDatos.Models.Response;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiCursoUsuarioListarReseñaService: IApiCursoUsuarioListarReseñaService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioListarReseñaService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoUsuarioListarReseñaResponse> obtenerCursoUsuarioListarReseña(int curId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioListarResena";
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;

            var ubicacion = new List<clsCursoUsuarioListarReseñaResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ubicacion.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (ubicacion == null)
            {
                return null;
            }
            //Si existe Material
            return ubicacion;
        }
        private clsCursoUsuarioListarReseñaResponse MapToValue(SqlDataReader reader)
        {
            return new clsCursoUsuarioListarReseñaResponse()
            {
                cuuId = Convert.ToInt32(reader["cuuId"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),
                cuuResena = reader["cuuResena"].ToString().Trim(),
                cuuScore = Convert.ToDecimal(reader["cuuScore"]),
                cuuFecha = Convert.ToDateTime(reader["cuuFecha"]),
            };

        }

    }
}
