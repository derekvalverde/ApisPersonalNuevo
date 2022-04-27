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
   public class ApiMaterialEvaluacionEliminarService: IApiMaterialEvaluacionEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialEvaluacionEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsMaterialEvaluacionEliminarResponse obtenerMaterialEvaluacionEliminar(int maeId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialEvaluacionEliminar";

            cmd.Parameters.Add("@maeId", System.Data.SqlDbType.Int).Value = maeId;

            var materialEvaluacion = new clsMaterialEvaluacionEliminarResponse();
            var reader = cmd.ExecuteReader();
            materialEvaluacion.eliminado = true;
            conn.Close();
            return materialEvaluacion;
        }
    }
}
