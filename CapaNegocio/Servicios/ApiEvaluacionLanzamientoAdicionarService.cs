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
    public class ApiEvaluacionLanzamientoAdicionarService: IApiEvaluacionLanzamientoAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionLanzamientoAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionLanzamientoAdicionarResponse obtenerEvaluacionLanzamientoAdicionar(int evaId, string evlEstado, DateTime evlFecha, int evaLimiteIntentos, DateTime evlFechaInicio,DateTime evlFechaFin, int usuId, int evlDuracion)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionLanzamientoAdicionar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            cmd.Parameters.Add("@evlEstado", System.Data.SqlDbType.Char,2).Value = evlEstado;
            cmd.Parameters.Add("@evlFecha", System.Data.SqlDbType.DateTime).Value = evlFecha;
            cmd.Parameters.Add("@evaLimiteIntentos", System.Data.SqlDbType.Int).Value = evaLimiteIntentos;
            cmd.Parameters.Add("@evlFechaInicio", System.Data.SqlDbType.DateTime).Value = evlFechaInicio;
            cmd.Parameters.Add("@evlFechaFin", System.Data.SqlDbType.DateTime).Value = evlFechaFin;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@evlDuracion", System.Data.SqlDbType.Int).Value = evlDuracion;
            var evaluacion = new clsEvaluacionLanzamientoAdicionarResponse();
            var reader = cmd.ExecuteReader();
            evaluacion.evaluacionLanzamientoAdicionado = true;
            conn.Close();
            return evaluacion;
        }
    }
}
