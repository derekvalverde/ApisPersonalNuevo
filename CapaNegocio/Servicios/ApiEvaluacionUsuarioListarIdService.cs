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
    public class ApiEvaluacionUsuarioListarIdService: IApiEvaluacionUsuarioListarIdService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionUsuarioListarIdService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEvaluacionUsuarioListarIdResponse> obtenerEvaluacionUsuarioListarId(int usuId, int evlId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionUsuarioListarId";           
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@evlId", System.Data.SqlDbType.Int).Value = evlId;
           
            var evaluacion = new List<clsEvaluacionUsuarioListarIdResponse>();
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
        private clsEvaluacionUsuarioListarIdResponse MapToValue(SqlDataReader reader)
        {
            return new clsEvaluacionUsuarioListarIdResponse()
            {
                evuId = Convert.ToInt32(reader["evuId"]),
                evuEstadoIntento = reader["evuEstadoIntento"].ToString().Trim(),
                evuNota = Convert.ToDecimal(reader["evuNota"]),
                evuNroIntentos = Convert.ToInt32(reader["evuNroIntentos"]),
                evuFecha = Convert.ToDateTime(reader["evuFecha"]),
            };

        }
    }
}
