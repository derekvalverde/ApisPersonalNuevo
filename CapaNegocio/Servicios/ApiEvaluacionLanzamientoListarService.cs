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
   public class ApiEvaluacionLanzamientoListarService: IApiEvaluacionLanzamientoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionLanzamientoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEvaluacionLanzamientoListarResponse> obtenerEvaluacionLanzamientoListar(int evaId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionLanzamientoListar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
          
            var evaluacion = new List<clsEvaluacionLanzamientoListarResponse>();
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
        private clsEvaluacionLanzamientoListarResponse MapToValue(SqlDataReader reader)
        {
            return new clsEvaluacionLanzamientoListarResponse()
            {
                evlId = Convert.ToInt32(reader["evlId"]),
                evlEstado = reader["evlEstado"].ToString().Trim(),
                evaLimiteIntentos = Convert.ToInt32(reader["evaLimiteIntentos"]),
                evlFechaInicio = Convert.ToDateTime(reader["evlFechaInicio"]),
                evlFechaFin = Convert.ToDateTime(reader["evlFechaFin"]),
                evlDuracion = Convert.ToInt32(reader["evlDuracion"]),
            };

        }
    }
}
