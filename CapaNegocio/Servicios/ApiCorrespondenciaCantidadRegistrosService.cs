using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiCorrespondenciaCantidadRegistrosService : IApiCorrespondenciaCantidadRegistrosService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaCantidadRegistrosService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaCantidadRegistrosResponse> obteneCorrespondenciaCantidadRegistros(string UsuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaCantidadRegistros";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.Int).Value = UsuCodigo;


            var cantidad = new List<clsCorrespondenciaCantidadRegistrosResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                cantidad.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (cantidad == null)
            {
                return null;
            }
            //Si existe Material


            return cantidad;

        }
        private clsCorrespondenciaCantidadRegistrosResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaCantidadRegistrosResponse()
            {
                corRegistros = Convert.ToInt32(reader["corRegistros"]),
               
            };

        }

    }
}
