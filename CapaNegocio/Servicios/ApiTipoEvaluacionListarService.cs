using CapaDatos.Data;
using CapaDatos.Models.Request;
using CapaDatos.Models.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiTipoEvaluacionListarService: IApiTipoEvaluacionListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiTipoEvaluacionListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsTipoEvaluacionlistarResponse> obtenerTipoEvaluacionlistar()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_TipoEvaluacionListar";

            var categoria = new List<clsTipoEvaluacionlistarResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categoria.Add(MapToValue(reader));
            }
            conn.Close();

            if (categoria == null)
            {
                return null;
            }

            return categoria;

        }
        private clsTipoEvaluacionlistarResponse MapToValue(SqlDataReader reader)
        {
            return new clsTipoEvaluacionlistarResponse()
            {
                tieId = Convert.ToInt32(reader["tieId"]),
                tieNombre = reader["tieNombre"].ToString().Trim(),               
            };

        }
    }
}
