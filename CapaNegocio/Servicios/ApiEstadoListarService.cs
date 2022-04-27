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
    public class ApiEstadoListarService: IApiEstadoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEstadoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEstadoListarResponse> obtenerEstado()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EstadoListar";

            var estados = new List<clsEstadoListarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                estados.Add(MapToValue(reader));
            }
            conn.Close();

            if (estados == null)
            {
                return null;
            }

            return estados;

        }
        private clsEstadoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEstadoListarResponse()
            {
                esaCodigo= reader["esaCodigo"].ToString().Trim(),
                esaNombre = reader["esaNombre"].ToString().Trim(),             
            };

        }
    }
}
