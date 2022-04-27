using CapaDatos.Data;
using CapaDatos.Models.Response;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiTipoCursoListarService: IApiTipoCursoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiTipoCursoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsTipoCursoListarResponse> obtenerTipoCursoListar()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_TipoCursoListar";

            var categoria = new List<clsTipoCursoListarResponse>();
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
        private clsTipoCursoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsTipoCursoListarResponse()
            {

                ticId = Convert.ToInt32(reader["ticId"]),
                ticNombre = reader["ticNombre"].ToString().Trim(),
              
            };

        }
    }
}
