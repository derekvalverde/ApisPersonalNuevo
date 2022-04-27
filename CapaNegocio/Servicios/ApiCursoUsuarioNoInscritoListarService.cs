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
    public class ApiCursoUsuarioNoInscritoListarService: IApiCursoUsuarioNoInscritoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioNoInscritoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoUsuarioNoInscritoListarResponse> obtenerCursoNoInscritoListar(int usuId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioNoInscritoListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var curso = new List<clsCursoUsuarioNoInscritoListarResponse>();
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
        private clsCursoUsuarioNoInscritoListarResponse MapToValue(SqlDataReader reader)
        {
            return new clsCursoUsuarioNoInscritoListarResponse()
            {
                curId = Convert.ToInt32(reader["curId"]),
                curTitulo = reader["curTitulo"].ToString().Trim(),
                curImagenDireccion = reader["curImagenDireccion"].ToString().Trim(),
                curEstado = reader["curEstado"].ToString().Trim(),
                curFecha = Convert.ToDateTime(reader["curFecha"]),
                curScore = Convert.ToDecimal(reader["curScore"]),
                ticNombre = reader["ticNombre"].ToString().Trim(),
            };

        }

    }
}
