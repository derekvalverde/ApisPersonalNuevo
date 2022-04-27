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
    public class ApiEvaluacionEliminarService: IApiEvaluacionEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionEliminarResponse obtenerEvaluacionEliminar(int evaId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionEliminar";

            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;

            var evaluacion = new clsEvaluacionEliminarResponse();
            var reader = cmd.ExecuteReader();
            evaluacion.eliminado = true;
            conn.Close();
            return evaluacion;
        }
    }
}
