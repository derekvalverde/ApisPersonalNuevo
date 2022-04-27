using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CapaNegocio.Servicios
{
    public class ApiAgenciaListarService : IApiAgenciaListarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiAgenciaListarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsAgenciaListarResponse> obtenerAgencia()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_AgenciaListar";

            var agencias = new List<clsAgenciaListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                agencias.Add(MapToValue(reader));
            }
            conn.Close();

            if (agencias == null)
            {
                return null;
            }

            return agencias;

        }
        private clsAgenciaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsAgenciaListarResponse()
            {

                ageId = reader["ageId"].ToString().Trim(),
                ageCodigo = reader["ageCodigo"].ToString().Trim(),
                ageNombre = reader["ageNombre"].ToString().Trim(),
                ageOficina = reader["ageOficina"].ToString().Trim(),
                ageCentro = reader["ageCentro"].ToString().Trim()

            };

        }

    }
}
