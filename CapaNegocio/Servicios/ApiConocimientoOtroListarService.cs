using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace CapaNegocio.Servicios
{
    public class ApiConocimientoOtroListarService : IApiConocimientoOtroListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiConocimientoOtroListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clcConocimientoOtroListarResponse> ObtenerUsuarioEstudiosOtro(int UsuId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "Api_ConocimientoOtroListar";

            cmd.Parameters.Add("@UsuId", System.Data.SqlDbType.Int).Value = UsuId;
          

            var informacion = new List<clcConocimientoOtroListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                informacion.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (informacion == null)
            {
                return null;
            }
            //Si existe Material


            return informacion;

        }
        private clcConocimientoOtroListarResponse MapToValue(SqlDataReader reader)
        {

            return new clcConocimientoOtroListarResponse()
            {
                cooId= Convert.ToInt32(reader["cooId"]),
                cooDescripcion = reader["cooDescrpcion"].ToString().Trim(),
             
            };

        }

    }
}
