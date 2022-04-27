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
    public class ApiEvaluacionUsuarioReseteoListarEstadoService: IApiEvaluacionUsuarioReseteoListarEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionUsuarioReseteoListarEstadoService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEvaluacionUsuarioReseteoListarEstadoResponse> obtenerEvaluacionReseteoListarEstado(int evlId, string eurEstado)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionUsuarioReseteoListarEstado";
            cmd.Parameters.Add("@evlId", System.Data.SqlDbType.Int).Value = evlId;
            cmd.Parameters.Add("@eurEstado", System.Data.SqlDbType.Char,2).Value = eurEstado;
            var curso = new List<clsEvaluacionUsuarioReseteoListarEstadoResponse>();
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
        private clsEvaluacionUsuarioReseteoListarEstadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsEvaluacionUsuarioReseteoListarEstadoResponse()
            {
                eurId = Convert.ToInt32(reader["eurId"]),
                eurDetalle = reader["eurDetalle"].ToString().Trim(),
                usuNombre = reader["usuNombre"].ToString().Trim(),
                uscNombre = reader["uscNombre"].ToString().Trim(),

            };

        }

    }
}
