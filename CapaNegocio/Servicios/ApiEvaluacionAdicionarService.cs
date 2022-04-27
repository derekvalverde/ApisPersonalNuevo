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
    public class ApiEvaluacionAdicionarService: IApiEvaluacionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionAdicionarResponse obtenerEvaluacionAdicionar(int modId, int tieId, string evaTitulo, string evaEstado, DateTime evaFecha,int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionAdicionar";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            cmd.Parameters.Add("@tieId", System.Data.SqlDbType.Int).Value = tieId;
            cmd.Parameters.Add("@evaTitulo", System.Data.SqlDbType.NVarChar, 60).Value = evaTitulo;
            cmd.Parameters.Add("@evaEstado", System.Data.SqlDbType.Char, 2).Value = evaEstado;                     
            cmd.Parameters.Add("@evaFecha", System.Data.SqlDbType.DateTime).Value = evaFecha;            
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var evaluacion = new clsEvaluacionAdicionarResponse();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                evaluacion = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (evaluacion == null)
            {
                return null;
            }
            return evaluacion;
        }
        private clsEvaluacionAdicionarResponse MapToValue(SqlDataReader reader)
        {
            return new clsEvaluacionAdicionarResponse()
            {
                evaId = Convert.ToInt32(reader["evaId"]),
            };

        }
    }
    
}
