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
    public class ApiUsuarioListarConfiguracionService:IApiUsuarioListarConfiguracionService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioListarConfiguracionService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioListarConfiguracionResponse> obtenerUsuarioConfiguracion(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioListarConfiguracion";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var material = new List<clsUsuarioListarConfiguracionResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                material.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (material == null)
            {
                return null;
            }

            return material;

        }
        private clsUsuarioListarConfiguracionResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioListarConfiguracionResponse()
            {
                ucnMAC = reader["ucnMAC"].ToString().Trim(),
               

            };

        }

    }
}
