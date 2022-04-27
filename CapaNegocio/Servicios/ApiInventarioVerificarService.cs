using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiInventarioVerificarService:IApiInventarioVerificarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiInventarioVerificarService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsInventarioVerificarResponse> obtenerInventarioVerificar(string actCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_InventarioVerificar";
            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.Int).Value = actCodigo;

            var activo = new List<clsInventarioVerificarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                activo.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (activo == null)
            {
                return null;
            }
            //Si existe Material


            return activo;

        }
        private clsInventarioVerificarResponse MapToValue(SqlDataReader reader)
        {

            return new clsInventarioVerificarResponse()
            {

                nombre = reader["nombre"].ToString().Trim(),
                fecha = Convert.ToDateTime(reader["fecha"])
               
            };

        }

    }
}
