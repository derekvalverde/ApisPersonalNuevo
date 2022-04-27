using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using WebIntiApi.Models;
using CapaDatos.Request;
using CapaDatos.Models;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioVisitaAcomponadaService : IApiUsuarioVisitaAcomponadaService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioVisitaAcomponadaService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioVisitaAcomponadaResponse> obtenerVisitaAcompañada(string usuId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioVisitaAcomponada";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var factura = new List<clsUsuarioVisitaAcomponadaResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                factura.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si la factura no tiene elementos
            if (factura == null)
            {
                return null;
            }

            return factura;

        }
        private clsUsuarioVisitaAcomponadaResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioVisitaAcomponadaResponse()
            {

                usuId = Convert.ToInt32(reader["usuId"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),

            };

        }
    }
}
