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
    public class ApiLeccionAdicionarService: IApiLeccionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiLeccionAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsLeccionAdicionarResponse obtenerLeccionAdicionar(int modId, string lecTitulo, string lecEstado, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LeccionAdicionar";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            cmd.Parameters.Add("@lecTitulo", System.Data.SqlDbType.NVarChar, 100).Value = lecTitulo;            
            cmd.Parameters.Add("@lecEstado", System.Data.SqlDbType.Char, 2).Value = lecEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var leccion = new clsLeccionAdicionarResponse();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                leccion = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (leccion == null)
            {
                return null;
            }

            return leccion;

        }
        private clsLeccionAdicionarResponse MapToValue(SqlDataReader reader)
        {

            return new clsLeccionAdicionarResponse()
            {

                lecId = Convert.ToInt32(reader["lecId"]),

            };
        }
    }

}
