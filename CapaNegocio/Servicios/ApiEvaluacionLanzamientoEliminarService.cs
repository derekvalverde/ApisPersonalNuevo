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
    public class ApiEvaluacionLanzamientoEliminarService: IApiEvaluacionLanzamientoEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionLanzamientoEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionLanzamientoEliminarResponse obtenerEvaluacionLanzamientoEliminar(int evlId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionLanzamientoEliminar";

            cmd.Parameters.Add("@evlId", System.Data.SqlDbType.Int).Value = evlId;

            var evaluacion = new clsEvaluacionLanzamientoEliminarResponse();
            var reader = cmd.ExecuteReader();
            evaluacion.eliminado = true;
            conn.Close();
            return evaluacion;
        }

    }
}
