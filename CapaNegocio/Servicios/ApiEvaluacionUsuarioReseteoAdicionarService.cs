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
    public class ApiEvaluacionUsuarioReseteoAdicionarService: IApiEvaluacionUsuarioReseteoAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionUsuarioReseteoAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionUsuarioReseteoAdicionarResponse obtenerEvaluacionUsuarioReseteoAdicionar(int cuuId, int evlId, string eurDetalle)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionUsuarioReseteoAdicionar";
            cmd.Parameters.Add("@cuuId", System.Data.SqlDbType.Int).Value = cuuId;
            cmd.Parameters.Add("@evlId", System.Data.SqlDbType.Int).Value = evlId;
            cmd.Parameters.Add("@eurDetalle", System.Data.SqlDbType.NVarChar, 300).Value = eurDetalle;
           

            var reseteo = new clsEvaluacionUsuarioReseteoAdicionarResponse();
            var reader = cmd.ExecuteReader();
            reseteo.evaluacionUsuarioReseteoAdicionado = true;
            conn.Close();
            return reseteo;
        }
    }
}
