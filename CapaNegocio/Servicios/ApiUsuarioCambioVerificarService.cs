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
    public class ApiUsuarioCambioVerificarService: IApiUsuarioCambioVerificarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioCambioVerificarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioCambioVerificarResponse verificaCambios(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_UsuarioCambioVerificar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var datosEmp = new clsUsuarioCambioVerificarResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosEmp = MapToValue(reader);


            }
            conn.Close();
            //
            //Si no exixte cliente
            if (datosEmp == null)
            {
                return null;
            }
            //Si existe cliente


            return datosEmp;

        }
        private clsUsuarioCambioVerificarResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioCambioVerificarResponse()
            {
                estado = reader["estado"].ToString().Trim(),
               
            };

        }
    }
}
