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
    public class ApiEvaluacionRespuestaUsuarioAdicionarService: IApiEvaluacionRespuestaUsuarioAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionRespuestaUsuarioAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionRespuestaUsuarioAdicionarResponse obtenerEvaluacionRespuestaUsuarioAdicionar(int resId, int evuId, string eruEstado)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionRespuestaUsuarioAdicionar";
            cmd.Parameters.Add("@resId", System.Data.SqlDbType.Int).Value = resId;
            cmd.Parameters.Add("@evuId", System.Data.SqlDbType.Int).Value = evuId;
            cmd.Parameters.Add("@eruEstado", System.Data.SqlDbType.Char, 2).Value = eruEstado;


            var usuarioRespuesta = new clsEvaluacionRespuestaUsuarioAdicionarResponse();
            var reader = cmd.ExecuteReader();
            usuarioRespuesta.usuarioRespuestaAdicionado = true;
            conn.Close();
            return usuarioRespuesta;
        }

    }
}
