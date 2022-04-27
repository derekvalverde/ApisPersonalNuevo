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
    public class ApiEvaluacionModificarService: IApiEvaluacionModificarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionModificarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionModificarResponse obtenerEvaluacionModificar(int evaId, int modId, int tieId, string evaTitulo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionModificar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            cmd.Parameters.Add("@tieId", System.Data.SqlDbType.Int).Value = tieId;
            cmd.Parameters.Add("@evaTitulo", System.Data.SqlDbType.NVarChar, 60).Value = evaTitulo;
            
            var evaluacion = new clsEvaluacionModificarResponse();
            var reader = cmd.ExecuteReader();
            evaluacion.evaluacionModificado = true;
            conn.Close();
            return evaluacion;
        }
    }
}
