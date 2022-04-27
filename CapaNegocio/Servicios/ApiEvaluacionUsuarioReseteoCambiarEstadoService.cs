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
    public class ApiEvaluacionUsuarioReseteoCambiarEstadoService: IApiEvaluacionUsuarioReseteoCambiarEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionUsuarioReseteoCambiarEstadoService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionUsuarioReseteoCambiarEstadoResponse obtenerEvaluacionUsuarioReseteoCambiarEstado(int eurId, int usuId, int evlId, string eurEstado)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionUsuarioReseteoCambiarEstado";
            cmd.Parameters.Add("@eurId", System.Data.SqlDbType.Int).Value = eurId;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@evlId", System.Data.SqlDbType.Int).Value = evlId;
            cmd.Parameters.Add("@eurEstado", System.Data.SqlDbType.Char, 2).Value = eurEstado;
                      

            var reseteo = new clsEvaluacionUsuarioReseteoCambiarEstadoResponse();
            var reader = cmd.ExecuteReader();
            reseteo.estadoCambiado = true;
            conn.Close();
            return reseteo;
        }

    }
}
