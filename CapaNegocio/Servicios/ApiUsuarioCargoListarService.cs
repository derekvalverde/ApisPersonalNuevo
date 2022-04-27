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
    public class ApiUsuarioCargoListarService: IApiUsuarioCargoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiUsuarioCargoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioCargoListarResponse> obtenerUsuarioCargo()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioCargoListar";

            var estados = new List<clsUsuarioCargoListarResponse>();
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
        private clsUsuarioCargoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioCargoListarResponse()
            {
                uniId = Convert.ToInt32(reader["uniId"]),
                uscNombre = reader["uscNombre"].ToString().Trim(),
            };

        }
    }
}
