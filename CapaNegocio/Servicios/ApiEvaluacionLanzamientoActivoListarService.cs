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
  public class ApiEvaluacionLanzamientoActivoListarService: IApiEvaluacionLanzamientoActivoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionLanzamientoActivoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEvaluacionLanzamientoActivoListarResponse> obtenerEvaluacionLanzamientoActivoListar(DateTime evlFecha)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionLanzamientoActivoListar";
            cmd.Parameters.Add("@evlFecha", System.Data.SqlDbType.DateTime).Value = evlFecha;
            var evaluacion = new List<clsEvaluacionLanzamientoActivoListarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                evaluacion.Add(MapToValue(reader));
            }
            conn.Close();

            if (evaluacion == null)
            {
                return null;
            }

            return evaluacion;

        }
        private clsEvaluacionLanzamientoActivoListarResponse MapToValue(SqlDataReader reader)
        {
            return new clsEvaluacionLanzamientoActivoListarResponse()
            {
                evaId = Convert.ToInt32(reader["evaId"]),
                evlEstado = reader["evlEstado"].ToString().Trim(),
                evaLimiteIntentos = Convert.ToInt32(reader["evaLimiteIntentos"]),
                evlFechaInicio = Convert.ToDateTime(reader["evlFechaInicio"]),
                evlFechaFin = Convert.ToDateTime(reader["evlFechaFin"]),
                evlDuracion= Convert.ToInt32(reader["evlDuracion"]),
            };
        }
    }
}
